using System.Text.Json.Serialization;

namespace ScaleUp.Integrations.Haravan.ProductVariantImages.Create;

public sealed record CreateHaravanVariantImageRequest
{
    [JsonPropertyName("productId")] public long ProductId { get; set; }
    [JsonPropertyName("image")] public HaravanVariantImageRequest Image { get; set; }
}

public sealed record HaravanVariantImageRequest
{
    [JsonPropertyName("variant_ids")]
    public List<long> VariantIds { get; set; }

    [JsonPropertyName("src")]
    public string Src { get; set; }

    [JsonPropertyName("filename")]
    public string Filename { get; set; }
}
