using System.Text.Json.Serialization;

namespace ScaleUp.Core.Api.Features.Orders.Dtos;

internal sealed record OrderCancelReasonDto
{
    [JsonPropertyName("code")]
    public required string Code { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("index")]
    public int? Index { get; set; }
}