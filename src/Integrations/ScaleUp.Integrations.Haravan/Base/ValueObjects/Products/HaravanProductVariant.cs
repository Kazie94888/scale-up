using System.Text.Json.Serialization;

namespace ScaleUp.Integrations.Haravan.Base.ValueObjects.Products;

public sealed record HaravanProductVariant
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    [JsonPropertyName("compare_at_price")]
    public decimal CompareAtPrice { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("fulfillment_service")]
    public object FulfillmentService { get; set; }

    [JsonPropertyName("grams")]
    public decimal Grams { get; set; }

    [JsonPropertyName("inventory_management")]
    public string InventoryManagement { get; set; }

    [JsonPropertyName("inventory_policy")]
    public string InventoryPolicy { get; set; }

    [JsonPropertyName("inventory_quantity")]
    public int InventoryQuantity { get; set; }

    [JsonPropertyName("position")]
    public int Position { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("product_id")]
    public int ProductId { get; set; }

    [JsonPropertyName("requires_shipping")]
    public bool RequiresShipping { get; set; }

    [JsonPropertyName("sku")]
    public object Sku { get; set; }

    [JsonPropertyName("taxable")]
    public bool Taxable { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("image_id")]
    public object ImageId { get; set; }

    [JsonPropertyName("option1")]
    public string Option1 { get; set; }

    [JsonPropertyName("option2")]
    public string Option2 { get; set; }

    [JsonPropertyName("option3")]
    public string Option3 { get; set; }

    [JsonPropertyName("inventory_advance")]
    public HaravanVariantInventoryAdvance InventoryAdvance { get; set; }    
}