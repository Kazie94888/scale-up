namespace ScaleUp.Core.Domain.Entities.Products;

public sealed record ProductVariant
{
    private ProductVariant()
    {
    }

    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Code { get; set; }
    public required string Sku { get; set; }
    public required decimal Price { get; set; }
    public decimal? OfferPrice { get; set; }
    public string Material { get; set; }
    public string Color { get; set; }
    public string Size { get; set; }
    public decimal Weight { get; set; }
    public int StockQuantity { get; set; }
    public string Dimensions { get; set; }
    public required string Status { get; set; }
    public List<ProductImage>? Images { get; set; }
    public List<ExternalId>? ExternalIds { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}