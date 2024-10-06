using ScaleUp.Core.Application.Integrations.Haravan.Features.Orders;
using ScaleUp.Core.Application.Integrations.Haravan.Features.Products;
using ScaleUp.Integrations.Haravan.Base.ValueObjects;

namespace ScaleUp.Core.Application.Integrations.Haravan;

public sealed class HaravanIntegrationService(
    HaravanOrderService haravanOrderService,
    HaravanProductService haravanProductService,
    HaravanIntegrationSettings integrationSettings)
    : IHaravanIntegrationService
{
    public async Task SyncOrders()
    {
        await haravanOrderService.SyncOrders(integrationSettings.TenantId);
    }

    public async Task MapOrders()
    {
        await haravanOrderService.MapOrders(integrationSettings.TenantId);
    }

    public async Task SyncProducts()
    {
        await haravanProductService.SyncProducts();
    }
}