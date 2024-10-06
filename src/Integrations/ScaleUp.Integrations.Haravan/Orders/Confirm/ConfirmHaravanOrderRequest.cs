using System.Text.Json.Serialization;
using Refit;

namespace ScaleUp.Integrations.Haravan.Orders.Confirm;

public class ConfirmHaravanOrderRequest
{
    [AliasAs("orderId")]
    [JsonPropertyName("order_id")]
    public string OrderId { get; set; }

    [JsonPropertyName("note")]
    public string Note { get; set; }
}