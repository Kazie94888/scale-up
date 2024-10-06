using System.ComponentModel;

namespace ScaleUp.Core.Domain.Enums;

public enum TenantActivationState
{
    [Description("Active")]
    Active,
    [Description("Active with limited time")]
    ActiveWithLimitedTime,
    [Description("Inactive")]
    Inactive
}