using System.Text.Json.Serialization;
using Refit;

namespace ScaleUp.Integrations.Haravan.Orders.Cancel;

public sealed class CancelHaravanOrderRequest
{
    [AliasAs("orderId")]
    [JsonPropertyName("order_id")]
    public string OrderId { get; set; }

    [JsonPropertyName("amount")]
    public string Amount { get; set; }


    [JsonPropertyName("note")]
    public string Note { get; set; }

    [JsonPropertyName("reason")]
    public string Reason { get; set; }
}
