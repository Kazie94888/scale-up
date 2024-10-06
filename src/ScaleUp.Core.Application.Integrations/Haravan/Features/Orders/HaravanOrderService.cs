using Microsoft.EntityFrameworkCore;
using MoreLinq;
using Newtonsoft.Json;
using ScaleUp.Core.Domain.Entities.Orders;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Enums;
using ScaleUp.Core.SharedKernel.Extensions;
using ScaleUp.Integrations.Haravan.ApiConsumers;
using ScaleUp.Integrations.Haravan.Orders;
using ScaleUp.Integrations.Haravan.Orders.GetList;

namespace ScaleUp.Core.Application.Integrations.Haravan.Features.Orders;

public sealed class HaravanOrderService(
    IHaravanOrderHubApi haravanOrderHubApi,
    HaravanDataContext haravanDataContext,
    MasterDataContext masterDataContext)
{
    //TODO: As Haravan store local date time, hence we need to add 12 hours
    private readonly DateTimeOffset To = DateTimeOffset.UtcNow.AddHours(12);

    public async Task SyncOrders(Guid tenantId)
    {
        var syncSetting = await haravanDataContext.HaravanSyncSettings.FirstOrDefaultAsync();
        if (syncSetting != null & syncSetting!.OrderSyncError.IsNotBlank()) return;

        var from = syncSetting.OrderSyncedAt;

        var currentPage = 1;
        while (true)
        {
            try
            {
                var request = new GetHaravanOrderRequest
                {
                    Page = currentPage,
                    CreatedAtMin = from.DateTime,
                    CreatedAtMax = To.DateTime
                };

                var rawOrders = await haravanOrderHubApi.GetOrders(
                    request.Page,
                    request.Limit,
                    request.CreatedAtMin,
                    request.CreatedAtMax);
                // TODO: handle HRV GetOrders error
                if (rawOrders.Orders.Count == 0) break;

                await SaveOrdersToJson(currentPage, request, rawOrders);

                var syncedOrders = rawOrders.Orders.Select(x => HaravanSyncedOrder.Create(x, tenantId));
                await haravanDataContext.HaravanOrders.AddRangeAsync(syncedOrders);

                syncSetting.OrderSyncedAt = rawOrders.Orders.Max(order => order.CreatedAt ?? DateTimeOffset.MinValue);
                haravanDataContext.HaravanSyncSettings.Update(syncSetting);

                await haravanDataContext.SaveChangesAsync();

                currentPage++;
            }
            catch (Exception ex)
            {
                syncSetting.OrderSyncError = ex.GetInnermostException().Message;
                haravanDataContext.HaravanSyncSettings.Update(syncSetting);
                await haravanDataContext.SaveChangesAsync();

                throw;
            }
        }
    }

    private async Task SaveOrdersToJson(int page, GetHaravanOrderRequest request, GetHaravanOrdersResponse rawOrders)
    {
        var folderPath = $"{Environment.CurrentDirectory}\\OrderLogs";
        var orderCount = rawOrders.Orders.Count;

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        var filePath = Path.Combine(folderPath,
            $"orders-{request.CreatedAtMin.ToFileDate()}-{request.CreatedAtMax.ToFileDate()}-page{page}-count{orderCount}.json");

        var jsonContent = JsonConvert.SerializeObject(rawOrders.Orders);

        await File.WriteAllTextAsync(filePath, jsonContent);
    }

    public async Task MapOrders(Guid tenantId)
    {
        var syncedOrders = await haravanDataContext.HaravanOrders
            .Where(x => x.Status == HaravanSyncedOrderStatus.New)
            .ToListAsync();

        var existedOrderCodes = await masterDataContext.Orders.Where(x => x.TenantId == tenantId).Select(x => x.Code).ToListAsync();
        syncedOrders = syncedOrders.Where(x => !existedOrderCodes.Any(y => y == x.Payload.OrderNumber)).ToList();

        if (!syncedOrders.Any()) return;

        var warehouses = await masterDataContext.Warehouses.ToListAsync();

        foreach (var batch in syncedOrders.Batch(100))
        {
            var orderBatch = new List<Order>();
            foreach (var syncedOrder in batch)
            {
                if (existedOrderCodes.Any(x => x == syncedOrder.Payload.OrderNumber))
                {
                    syncedOrder.Status = HaravanSyncedOrderStatus.Synced;
                    continue;
                }

                var warehouse = warehouses.FirstOrDefault(x => x.Code == syncedOrder.Payload.LocationId.ToString());
                var order = HaravanOrderMapper.Map(syncedOrder.Payload, warehouse, tenantId);
                syncedOrder.Status = HaravanSyncedOrderStatus.Synced;

                existedOrderCodes.Add(order.Code);
                orderBatch.Add(order);
            }

            if (orderBatch.Any())
            {
                await masterDataContext.Orders.AddRangeAsync(orderBatch);
                await masterDataContext.SaveChangesAsync();

                haravanDataContext.HaravanOrders.UpdateRange(batch);
                await haravanDataContext.SaveChangesAsync();
            }
        }
    }
}