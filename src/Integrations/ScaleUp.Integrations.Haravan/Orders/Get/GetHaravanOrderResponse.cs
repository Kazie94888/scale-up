using ScaleUp.Integrations.Haravan.Base.ValueObjects.Orders;
using System.Text.Json.Serialization;

namespace ScaleUp.Integrations.Haravan.Orders.Get
{
    public sealed record GetHaravanOrderResponse
    {
        [JsonPropertyName("order")] public HaravanOrder Order { get; set; }
    }
}