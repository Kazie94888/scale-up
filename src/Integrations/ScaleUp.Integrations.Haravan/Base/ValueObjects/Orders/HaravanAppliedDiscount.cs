using System.Text.Json.Serialization;

namespace ScaleUp.Integrations.Haravan.Base.ValueObjects.Orders;

public sealed record HaravanAppliedDiscount
{
    [JsonPropertyName("description")] public string Description { get; set; }
    [JsonPropertyName("amount")] public decimal? Amount { get; set; }
}