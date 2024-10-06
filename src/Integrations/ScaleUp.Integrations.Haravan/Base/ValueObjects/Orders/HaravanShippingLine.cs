using System.Text.Json.Serialization;

namespace ScaleUp.Integrations.Haravan.Base.ValueObjects.Orders;

public sealed record HaravanShippingLine
{
    [JsonPropertyName("code")] public string Code { get; set; }
    [JsonPropertyName("price")] public decimal? Price { get; set; }
    [JsonPropertyName("source")] public string Source { get; set; }
    [JsonPropertyName("title")] public string Title { get; set; }
}