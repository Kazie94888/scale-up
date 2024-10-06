using ScaleUp.Core.Api.Features.Orders.Dtos;
using System.Text.Json.Serialization;

namespace ScaleUp.Core.Api.Features.Orders.GetList;

internal sealed record GetOrderListResponse
{
    internal GetOrderListResponse() { }
    internal GetOrderListResponse(IEnumerable<OrderDto>? data)
    {
        Data = data;
    }

    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("data")]
    public IEnumerable<OrderDto>? Data { get; set; }
}
