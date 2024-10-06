using ScaleUp.Core.Api.Features.Orders.Dtos;

namespace ScaleUp.Core.Api.Features.Orders.CancelReasons.GetList;

internal sealed record GetOrderCancelReasonListResponse
{
    public required List<OrderCancelReasonDto> Data { get; init; }
}
