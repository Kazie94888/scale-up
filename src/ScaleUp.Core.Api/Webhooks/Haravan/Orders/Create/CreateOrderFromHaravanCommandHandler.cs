using FluentResults;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Application.Integrations.Haravan.Features.Orders;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Messaging;
using ScaleUp.Integrations.Haravan.ApiConsumers;

namespace ScaleUp.Core.Api.Webhooks.Haravan.Orders.Create;

internal sealed class CreateOrderFromHaravanCommandHandler(MasterDataContext dataContext, IHaravanOrderHubApi orderHubApi) : ICommandHandler<CreateOrderFromHaravanCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateOrderFromHaravanCommand command, CancellationToken cancellationToken)
    {
        var tenants = await dataContext.Tenants.ToListAsync();
        var tenant = await dataContext.Tenants.FirstAsync(x => x.HaravanIntegrationConfig.OrgId == command.WebHookInfo.OrgId);
        
        var response = await orderHubApi.GetOrder(command.WebHookInfo.OrderId);

        if (!response.IsSuccessStatusCode)
            return Result.Fail("Failed to get order from Haravan.");
        var warehouse = await dataContext.Warehouses.FirstAsync(x => x.Code == response.Content.Order.LocationId.ToString());

        var order = HaravanOrderMapper.Map(response.Content.Order, warehouse, tenant.Id);

        dataContext.Orders.Add(order);

        await dataContext.SaveChangesAsync();

        return Result.Ok(order.Id);



    }
}
