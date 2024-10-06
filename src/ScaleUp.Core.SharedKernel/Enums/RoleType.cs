using System.ComponentModel;

namespace ScaleUp.Core.SharedKernel.Enums;

public enum RoleType
{
    [Description("System")]
    System,
    [Description("Tenant")]
    Tenant
}