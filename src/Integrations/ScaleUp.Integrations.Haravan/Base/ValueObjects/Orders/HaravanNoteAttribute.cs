using System.Text.Json.Serialization;

namespace ScaleUp.Integrations.Haravan.Base.ValueObjects.Orders;

public sealed record HaravanNoteAttribute
{
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("value")] public string Value { get; set; }
}