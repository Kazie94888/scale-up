using ScaleUp.Core.SharedKernel.Base;
using ScaleUp.Core.SharedKernel.Extensions;

namespace ScaleUp.Core.Domain.Enums;

public sealed class OrderAction(string name, int value) : SmartEnumBase<OrderAction>(name, value)
{
    public static OrderAction Create = new(nameof(Create).ToSnakeCase(), 1);
    public static OrderAction CreateOnHaravan = new(nameof(CreateOnHaravan).ToSnakeCase(), 2);
    public static OrderAction Confirm = new(nameof(Confirm).ToSnakeCase(), 3);
    public static OrderAction Pick = new(nameof(Pick).ToSnakeCase(), 4);
    public static OrderAction Pack = new(nameof(Pack).ToSnakeCase(), 5);
    public static OrderAction Cancel = new(nameof(Cancel).ToSnakeCase(), 6);
    public static OrderAction Close = new(nameof(Close).ToSnakeCase(), 7);
    public static OrderAction ChangeStatus = new(nameof(ChangeStatus).ToSnakeCase(), 8);
    public static OrderAction ConfirmPayment = new(nameof(ConfirmPayment).ToSnakeCase(), 9);
    public static OrderAction Deliver = new(nameof(Deliver).ToSnakeCase(), 10);
    public static OrderAction Sync = new(nameof(Sync).ToSnakeCase(), 11);
}