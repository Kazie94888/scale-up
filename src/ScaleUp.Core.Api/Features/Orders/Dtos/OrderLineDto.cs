using ScaleUp.Core.Api.Shared.Dtos;
using System.Text.Json.Serialization;

namespace ScaleUp.Core.Api.Features.Orders.Dtos;

internal sealed record OrderLineDto
{
    [JsonPropertyName("item_code")]
    public string? ItemCode { get; set; }

    [JsonPropertyName("item_name")]
    public required string ItemName { get; set; }

    [JsonPropertyName("qty")]
    public required int Quantity { get; set; }

    [JsonPropertyName("sku")]
    public string? Sku { get; set; }

    [JsonPropertyName("line_total")]
    public decimal? LineTotal { get; set; }

    [JsonPropertyName("list_price")]
    public decimal? ListPrice { get; set; }

    [JsonPropertyName("price")]
    public required decimal Price { get; set; }

    [JsonPropertyName("discount_amount")]
    public decimal? DiscountAmount { get; set; }

    [JsonPropertyName("image_url")]
    public string? ImageUrl { get; set; }

    [JsonPropertyName("variants")]
    public IEnumerable<VariantDto>? Variants { get; set; }
}
