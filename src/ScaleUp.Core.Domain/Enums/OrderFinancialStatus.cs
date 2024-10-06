using System.ComponentModel;

namespace ScaleUp.Core.Domain.Enums;

public enum OrderFinancialStatus
{
    [Description("paid")]
    Paid,

    [Description("unpaid")]
    Unpaid
}
