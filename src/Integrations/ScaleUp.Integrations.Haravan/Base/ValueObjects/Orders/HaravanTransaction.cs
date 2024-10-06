using System.Text.Json.Serialization;

namespace ScaleUp.Integrations.Haravan.Base.ValueObjects.Orders;

public sealed record HaravanTransaction
{
    [JsonPropertyName("amount")] public decimal Amount { get; set; }
    [JsonPropertyName("authorization")] public string Authorization { get; set; }
    [JsonPropertyName("created_at")] public DateTime CreatedAt { get; set; }
    [JsonPropertyName("device_id")] public string DeviceId { get; set; }
    [JsonPropertyName("gateway")] public string Gateway { get; set; }
    [JsonPropertyName("id")] public long? Id { get; set; }
    [JsonPropertyName("kind")] public string Kind { get; set; }
    [JsonPropertyName("order_id")] public long? OrderId { get; set; }
    [JsonPropertyName("receipt")] public string Receipt { get; set; }
    [JsonPropertyName("status")] public string Status { get; set; }
    [JsonPropertyName("user_id")] public long UserId { get; set; }
    [JsonPropertyName("location_id")] public long? LocationId { get; set; }
    [JsonPropertyName("payment_details")] public string PaymentDetails { get; set; }
    [JsonPropertyName("parent_id")] public long? ParentId { get; set; }
    [JsonPropertyName("currency")] public string Currency { get; set; }

    [JsonPropertyName("haravan_transaction_id")]
    public string HaravanTransactionId { get; set; }

    [JsonPropertyName("external_transaction_id")]
    public string ExternalTransactionId { get; set; }
}