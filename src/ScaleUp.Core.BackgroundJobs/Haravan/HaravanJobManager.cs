using Hangfire;
using ScaleUp.Core.Application.Integrations.Haravan;

namespace ScaleUp.Core.BackgroundJobs.Haravan;

internal interface IHaravanJobManager
{
    Task SyncOrdersTrigger();
    Task MapOrdersTrigger();
    Task SyncProductsTrigger();
}

internal class HaravanJobManager(ILogger<HaravanJobManager> logger, IHaravanIntegrationService haravanIntegrationService)
    : IHaravanJobManager
{
    [DisableConcurrentExecution(60 * 60 * 24)]
    public async Task SyncOrdersTrigger()
    {
        await haravanIntegrationService.SyncOrders();
    }

    [DisableConcurrentExecution(60 * 60 * 24)]
    public async Task MapOrdersTrigger()
    {
        await haravanIntegrationService.MapOrders();
    }

    [DisableConcurrentExecution(60 * 60 * 24)]
    public async Task SyncProductsTrigger()
    {
        await haravanIntegrationService.SyncProducts();
    }
}