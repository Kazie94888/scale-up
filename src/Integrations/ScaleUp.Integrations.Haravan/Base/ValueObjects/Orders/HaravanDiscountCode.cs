using System.Text.Json.Serialization;

namespace ScaleUp.Integrations.Haravan.Base.ValueObjects.Orders;

public sealed record HaravanDiscountCode
{
    [JsonPropertyName("amount")] public decimal? Amount { get; set; }
    [JsonPropertyName("code")] public string Code { get; set; }
    [JsonPropertyName("type")] public string Type { get; set; }
    [JsonPropertyName("is_coupon_code")] public bool? IsCouponCode { get; set; }
}