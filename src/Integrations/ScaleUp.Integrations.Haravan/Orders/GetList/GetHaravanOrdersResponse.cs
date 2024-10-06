using ScaleUp.Integrations.Haravan.Base.ValueObjects.Orders;
using System.Text.Json.Serialization;

namespace ScaleUp.Integrations.Haravan.Orders.GetList
{
    public sealed record GetHaravanOrdersResponse
    {
        [JsonPropertyName("orders")] public List<HaravanOrder> Orders { get; set; }
    }
}