using System.Text.Json.Serialization;
using ScaleUp.Integrations.Haravan.Base.ValueObjects.Products;

namespace ScaleUp.Integrations.Haravan.Products.GetList;

public sealed record GetHaravanProductResponse
{
    [JsonPropertyName("products")] public List<HaravanProduct> Products { get; set; }
}