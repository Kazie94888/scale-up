namespace ScaleUp.Core.Api.Features.Orders.Confirm;

internal sealed class ConfirmOrderResponse
{
    public required Guid OrderId { get; init; }
}