using Refit;
using System.Text.Json.Serialization;

namespace ScaleUp.Integrations.Haravan.Orders.Payments;

public sealed class ConfirmHaravanOrderPaymentRequest
{
    [AliasAs("orderId")]
    [JsonPropertyName("order_id")]
    public string OrderId { get; set; }

    public Transaction Transaction { get; set; }
}

public sealed class Transaction
{
    [JsonPropertyName("amount")]
    public string Amount { get; set; }


    [JsonPropertyName("kind")]
    public string Kind { get; set; }
}
