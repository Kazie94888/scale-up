using System.ComponentModel;

namespace ScaleUp.Core.Domain.Enums;

public enum PaymentStatus
{
    [Description("paid")]
    Paid,

    [Description("unpaid")]
    Unpaid,

    [Description("BankDeposit")]
    BankDeposit,
}