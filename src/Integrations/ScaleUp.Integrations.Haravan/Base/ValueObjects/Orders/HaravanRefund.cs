using System.Text.Json.Serialization;

namespace ScaleUp.Integrations.Haravan.Base.ValueObjects.Orders;

public sealed record HaravanRefund
{
    [JsonPropertyName("created_at")] public DateTime? CreatedAt { get; set; }
    [JsonPropertyName("id")] public long? Id { get; set; }
    [JsonPropertyName("note")] public string Note { get; set; }

    [JsonPropertyName("refund_line_items")]
    public List<HaravanRefundLineItem> RefundLineItems { get; set; }

    [JsonPropertyName("restock")] public bool? Restock { get; set; }
    [JsonPropertyName("user_id")] public long UserId { get; set; }
    [JsonPropertyName("order_id")] public long? OrderId { get; set; }
    [JsonPropertyName("location_id")] public long? LocationId { get; set; }
    [JsonPropertyName("transactions")] public List<HaravanTransaction> Transactions { get; set; }
}

public sealed record HaravanRefundLineItem
{
    [JsonPropertyName("id")] public long? Id { get; set; }
    [JsonPropertyName("line_item")] public HaravanOrderLineItem OrderLineItem { get; set; }
    [JsonPropertyName("line_item_id")] public long? LineItemId { get; set; }
    [JsonPropertyName("quantity")] public long? Quantity { get; set; }
}