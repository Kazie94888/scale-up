using System.ComponentModel;

namespace ScaleUp.Core.Domain.Enums;

public enum PaymentMethod
{
    [Description("Cod")]
    Cod,
    [Description("Momo")]
    Momo,
    [Description("Other")]
    Other
}