using System.Text.Json.Serialization;
using ScaleUp.Integrations.Haravan.Base.ValueObjects.Products;

namespace ScaleUp.Integrations.Haravan.Products.Create;

public sealed class CreateHaravanProductRequest
{
    [JsonPropertyName("product")] public HaravanProduct Product { get; set; }
}