using System.Text.Json.Serialization;

namespace ScaleUp.Integrations.Haravan.Base.ValueObjects.Products;

public sealed record HaravanProductImage
{
    [JsonPropertyName("src")]
    public string Src { get; set; }

    [JsonPropertyName("filename")]
    public string Filename { get; set; }
    
    [JsonPropertyName("created_at")] 
    public DateTime? CreatedAt { get; set; }
    
    [JsonPropertyName("updated_at")] 
    public DateTime? UpdatedAt { get; set; }
    
    [JsonPropertyName("variant_ids")]
    public List<long> VariantIds { get; set; }
}