namespace ScaleUp.Core.Application.Integrations.Haravan;

public interface IHaravanIntegrationService
{
    Task SyncOrders();
    Task MapOrders();
    Task SyncProducts();
}