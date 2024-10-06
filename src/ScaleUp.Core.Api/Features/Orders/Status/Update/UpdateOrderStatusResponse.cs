namespace ScaleUp.Core.Api.Features.Orders.Status.Update;

internal sealed class UpdateOrderStatusResponse
{
    public required Guid OrderId { get; init; }
}