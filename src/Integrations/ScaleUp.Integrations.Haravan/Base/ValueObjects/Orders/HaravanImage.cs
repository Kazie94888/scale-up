using System.Text.Json.Serialization;

namespace ScaleUp.Integrations.Haravan.Base.ValueObjects.Orders;

public sealed record HaravanImage
{
    [JsonPropertyName("src")] public string Src { get; set; }
}