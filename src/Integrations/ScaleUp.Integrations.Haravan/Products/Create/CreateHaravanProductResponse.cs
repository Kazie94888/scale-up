using System.Text.Json.Serialization;
using ScaleUp.Integrations.Haravan.Base.ValueObjects.Products;

namespace ScaleUp.Integrations.Haravan.Products.Create;

public sealed class CreateHaravanProductResponse
{
    [JsonPropertyName("product")] public HaravanProduct Product { get; set; }
}