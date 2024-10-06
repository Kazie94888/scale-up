using Hangfire;
using Humanizer;

namespace ScaleUp.Core.BackgroundJobs.Haravan;

internal static class HaravanJobScheduler
{
    internal static void Schedule()
    {
        var recurringJobOptions = new RecurringJobOptions
        {
            TimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
        };
        RecurringJob.AddOrUpdate<IHaravanJobManager>(nameof(IHaravanJobManager.SyncOrdersTrigger).Kebaberize(), x => x.SyncOrdersTrigger(), Cron.Hourly());
        RecurringJob.AddOrUpdate<IHaravanJobManager>(nameof(IHaravanJobManager.MapOrdersTrigger).Kebaberize(), x => x.MapOrdersTrigger(), Cron.Hourly());
        RecurringJob.AddOrUpdate<IHaravanJobManager>(nameof(IHaravanJobManager.SyncProductsTrigger).Kebaberize(), x => x.SyncProductsTrigger(), Cron.Never());
    }
}