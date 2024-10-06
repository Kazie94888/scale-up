namespace ScaleUp.Core.Api.Features.Orders.Payments.Confirm;

internal sealed class ConfirmOrderPaymentResponse
{
    public required Guid OrderId { get; init; }
}