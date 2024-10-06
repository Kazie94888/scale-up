using System.Text.Json.Serialization;

namespace ScaleUp.Integrations.Haravan.Base.ValueObjects.Products;

public sealed record HaravanProduct
{
    [JsonPropertyName("body_html")]
    public string BodyHtml { get; set; }

    [JsonPropertyName("body_plain")]
    public object BodyPlain { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("handle")]
    public string Handle { get; set; }

    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("images")]
    public List<HaravanProductImage> Images { get; set; }

    [JsonPropertyName("product_type")]
    public string ProductType { get; set; }

    [JsonPropertyName("published_at")]
    public DateTime? PublishedAt { get; set; }

    [JsonPropertyName("published_scope")]
    public string PublishedScope { get; set; }

    [JsonPropertyName("tags")]
    public string Tags { get; set; }

    [JsonPropertyName("template_suffix")]
    public string TemplateSuffix { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("variants")]
    public List<HaravanProductVariant> Variants { get; set; }

    [JsonPropertyName("vendor")]
    public string Vendor { get; set; }

    [JsonPropertyName("options")]
    public List<HaravanProductOption> Options { get; set; }

    [JsonPropertyName("only_hide_from_list")]
    public bool OnlyHideFromList { get; set; }

    [JsonPropertyName("not_allow_promotion")]
    public bool NotAllowPromotion { get; set; }
        
    [JsonPropertyName("published")]
    public bool? Published { get; set; }
}