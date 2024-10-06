using ScaleUp.Core.Api.Shared.Dtos;
using System.Text.Json.Serialization;

namespace ScaleUp.Core.Api.Features.Orders.Dtos;

internal sealed record WarehouseDto
{

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("source")]
    public string? Source { get; set; }

    [JsonPropertyName("address_line_1")]
    public string? AddressLine1 { get; set; }

    [JsonPropertyName("ward")]
    public LocationDto? Ward { get; set; }

    [JsonPropertyName("district")]
    public LocationDto? District { get; set; }

    [JsonPropertyName("province")]
    public LocationDto? Province { get; set; }

    [JsonPropertyName("country")]
    public LocationDto? Country { get; set; }
}
