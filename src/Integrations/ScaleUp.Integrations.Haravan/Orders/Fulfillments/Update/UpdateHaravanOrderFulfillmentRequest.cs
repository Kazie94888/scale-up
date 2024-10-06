using System.Text.Json.Serialization;
using Refit;

namespace ScaleUp.Integrations.Haravan.Orders.Fulfillments.Update;

public sealed class UpdateHaravanOrderFulfillmentRequest
{
    [AliasAs("orderId")]
    [JsonPropertyName("order_id")]
    public string OrderId { get; set; }
    
    [AliasAs("fulfillmentId")]
    [JsonPropertyName("fulfillment_id")]
    public string FulfillmentId { get; set; }
    
    [JsonPropertyName("fulfillment")]
    public Fullfillment FullfillmentRequest { get; set; }
    
    
    public sealed class Fullfillment
    {
        [JsonPropertyName("tracking_number")]
        public string TrackingNumber { get; set; }
    }
}