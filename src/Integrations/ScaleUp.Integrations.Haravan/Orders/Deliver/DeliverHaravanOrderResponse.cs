using System.Text.Json.Serialization;
using ScaleUp.Integrations.Haravan.Base.ValueObjects.Orders;

namespace ScaleUp.Integrations.Haravan.Orders.Deliver;

public class DeliverHaravanOrderResponse
{
    [JsonPropertyName("fulfillment")]
    public HaravanFulfillment Fulfillment { get; set; }
}