namespace ScaleUp.Core.Api.Features.Orders.Cancel;

internal sealed record CancelOrderResponse
{
    public required Guid OrderId { get; init; }
}