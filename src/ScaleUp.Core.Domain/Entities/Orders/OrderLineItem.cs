using ScaleUp.Core.Domain.Shared;

namespace ScaleUp.Core.Domain.Entities.Orders;

public sealed record OrderLineItem
{
    public string OrderCode { get; set; }
    public string? Sku { get; set; }
    public string? Barcode { get; set; }
    public string? ItemCode { get; set; }
    public string ItemName { get; set; }
    public string? ItemCategory { get; set; }
    public decimal? ListPrice { get; set; }
    public decimal? SalesPrice { get; set; }
    public decimal Price { get; set; }
    public decimal? DiscountAmount { get; set; }
    public int Quantity { get; set; }
    public List<Variant>? Variants { get; set; }
    public decimal? LineTotal { get; set; }
    public string? RefPromotion { get; set; }
    public string? ProductId { get; set; }
    public string? ImageUrl { get; set; }

    private OrderLineItem()
    {

    }

    public OrderLineItem(
    string orderCode,
    string? sku,
    string? barcode,
    string? itemCode,
    string itemName,
    string? itemCategory,
    decimal? listPrice,
    decimal? salesPrice,
    decimal price,
    decimal? discountAmount,
    int quantity,
    List<Variant>? variants,
    decimal? lineTotal,
    string? refPromotion,
    string? productId,
    string? imageUrl)
    {
        OrderCode = orderCode;
        Sku = sku;
        Barcode = barcode;
        ItemCode = itemCode;
        ItemName = itemName;
        ItemCategory = itemCategory;
        ListPrice = listPrice;
        SalesPrice = salesPrice;
        Price = price;
        DiscountAmount = discountAmount;
        Quantity = quantity;
        Variants = variants;
        LineTotal = lineTotal;
        RefPromotion = refPromotion;
        ProductId = productId;
        ImageUrl = imageUrl;
    }
}
