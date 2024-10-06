namespace ScaleUp.Core.Domain.Enums;

public enum OrderPaymentKindTransaction
{
    Pending,
    Authorization,
    Capture,
    Sale,
    Void,
    Refund
}