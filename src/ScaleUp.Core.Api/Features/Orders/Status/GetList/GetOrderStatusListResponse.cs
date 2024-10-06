using ScaleUp.Core.Api.Features.Orders.Dtos;

namespace ScaleUp.Core.Api.Features.Orders.Status.GetList;

internal sealed record GetOrderStatusListResponse
{
    public required List<OrderStatusDto> Data { get; set; }
}
