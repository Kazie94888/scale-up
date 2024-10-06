using System.Text.Json.Serialization;

namespace ScaleUp.Integrations.Haravan.Base.ValueObjects.Orders;

public sealed record HaravanRedeemModel
{
    [JsonPropertyName("payload_id")] public long? PayloadId { get; set; }
    [JsonPropertyName("used_amount")] public decimal? UsedAmount { get; set; }
    [JsonPropertyName("transaction_id")] public long? TransactionId { get; set; }
    [JsonPropertyName("discount_type")] public string DiscountType { get; set; }
    [JsonPropertyName("discount")] public decimal? Discount { get; set; }
    [JsonPropertyName("max_per_order")] public decimal? MaxPerOrder { get; set; }
    [JsonPropertyName("amount")] public decimal? Amount { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
}