using System.Text.Json.Serialization;

namespace ScaleUp.Integrations.Haravan.Base.ValueObjects.Orders;

public sealed record HaravanOrderLineItem
{
    [JsonPropertyName("fulfillable_quantity")]
    public int? FulfillableQuantity { get; set; }

    [JsonPropertyName("fulfillment_service")]
    public string FulfillmentService { get; set; }

    [JsonPropertyName("fulfillment_status")]
    public string FulfillmentStatus { get; set; }

    [JsonPropertyName("grams")] public decimal? Grams { get; set; }
    [JsonPropertyName("id")] public long? Id { get; set; }
    [JsonPropertyName("price")] public decimal? Price { get; set; }
    [JsonPropertyName("product_id")] public int? ProductId { get; set; }
    [JsonPropertyName("quantity")] public int? Quantity { get; set; }

    [JsonPropertyName("requires_shipping")]
    public bool? RequiresShipping { get; set; }

    [JsonPropertyName("sku")] public string Sku { get; set; }
    [JsonPropertyName("title")] public string Title { get; set; }
    [JsonPropertyName("variant_id")] public int? VariantId { get; set; }
    [JsonPropertyName("variant_title")] public string VariantTitle { get; set; }
    [JsonPropertyName("vendor")] public string Vendor { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("variant_inventory_management")]
    public string VariantInventoryManagement { get; set; }

    [JsonPropertyName("properties")] public List<HaravanProperty> Properties { get; set; }
    [JsonPropertyName("product_exists")] public bool? ProductExists { get; set; }
    [JsonPropertyName("price_original")] public decimal? PriceOriginal { get; set; }
    [JsonPropertyName("price_promotion")] public decimal? PricePromotion { get; set; }
    [JsonPropertyName("type")] public string Type { get; set; }
    [JsonPropertyName("gift_card")] public bool? GiftCard { get; set; }
    [JsonPropertyName("taxable")] public bool? Taxable { get; set; }
    [JsonPropertyName("tax_lines")] public string TaxLines { get; set; }
    [JsonPropertyName("barcode")] public string Barcode { get; set; }

    [JsonPropertyName("applied_discounts")]
    public List<HaravanAppliedDiscount> AppliedDiscounts { get; set; }

    [JsonPropertyName("total_discount")] public decimal? TotalDiscount { get; set; }
    [JsonPropertyName("image")] public HaravanImage Image { get; set; }

    [JsonPropertyName("not_allow_promotion")]
    public bool? NotAllowPromotion { get; set; }

    [JsonPropertyName("ma_cost_amount")] public decimal? MaCostAmount { get; set; }
    [JsonPropertyName("actual_price")] public decimal? ActualPrice { get; set; }

    [JsonPropertyName("discount_allocations")]
    public decimal? DiscountAllocations { get; set; }
}