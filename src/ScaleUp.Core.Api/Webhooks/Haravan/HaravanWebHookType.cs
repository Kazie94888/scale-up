using ScaleUp.Core.SharedKernel.Base;

namespace ScaleUp.Core.Api.Webhooks.Haravan;

public sealed class HaravanWebHookType(string name, int value) : SmartEnumBase<HaravanWebHookType>(name, value)
{
    public static HaravanWebHookType OrderCreated = new("orders/create", 1);
    public static HaravanWebHookType OrderUpdated = new("orders/update", 2);
    public static HaravanWebHookType OrderCancelled = new("orders/cancel", 3);
    public static HaravanWebHookType OrderCompleted = new("orders/complete", 4);
    public static HaravanWebHookType OrderDeleted = new("orders/delete", 5);
}
