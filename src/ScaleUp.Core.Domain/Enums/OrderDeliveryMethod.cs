using System.ComponentModel;

namespace ScaleUp.Core.Domain.Enums;

public enum OrderDeliveryMethod
{
    [Description("Fast")]
    Fast,

    [Description("Customer self pick")]
    CustomerSelfPick
}