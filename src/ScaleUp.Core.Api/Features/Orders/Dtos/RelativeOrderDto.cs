using System.Text.Json.Serialization;

namespace ScaleUp.Core.Api.Features.Orders.Dtos;

internal sealed record RelativeOrderDto
{
    [JsonPropertyName("order_id")]
    public required Guid OrderId { get; set; }


    [JsonPropertyName("order_code")]
    public string? OrderCode { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
