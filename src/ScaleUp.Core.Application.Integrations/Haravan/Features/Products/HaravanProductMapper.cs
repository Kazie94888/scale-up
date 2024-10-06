using ScaleUp.Core.Domain.Entities.Products;
using ScaleUp.Integrations.Haravan.Base.ValueObjects.Products;

namespace ScaleUp.Core.Application.Integrations.Haravan.Features.Products;

internal static class HaravanProductMapper
{
    internal static HaravanProduct Map(Product product, ProductCategory? category, Vendor? vendor)
    {
        var variants = GetProductVariants(product);
        var images = GetProductImages(product);
        
        return new HaravanProduct
        {
            Title = product.Name,
            BodyHtml = product.Description,
            Vendor = vendor!.Name,
            ProductType = category!.Name,
            // TODO: move this to a config
            Published = false,
            Images = images,
            Variants = variants,
            Options =
            [
                new HaravanProductOption { Name = "Size", },
                new HaravanProductOption { Name = "Color", }
            ]
        };
    }
    
    private static List<HaravanProductVariant> GetProductVariants(Product product)
    {
        if (product.Variants == null || !product.Variants.Any())
            return [];
        
        return product.Variants.Select(x => new HaravanProductVariant
        {
            Title = x.Name,
            Price = x.Price,
            Sku = x.Sku,
            Barcode = x.Code,
            InventoryManagement = "scale-up",
            InventoryQuantity = x.StockQuantity,
            Grams = x.Weight,
            Option1 = x.Size,
            Option2 = x.Color,
        }).ToList();
    }
    
    private static List<HaravanProductImage> GetProductImages(Product product)
    {
        if (product.Variants == null || !product.Variants.Any())
            return [];
        
        return product.Images!.Select(x => new HaravanProductImage
        {
            Src = x.Src,
            Filename = x.Filename
        }).ToList();
    }
}