using MoreLinq;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Domain.Entities.Products;
using ScaleUp.Core.Domain.Enums;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Extensions;
using ScaleUp.Integrations.Haravan.ApiConsumers;
using ScaleUp.Integrations.Haravan.Products.Create;
using ScaleUp.Integrations.Haravan.ProductVariantImages.Create;

namespace ScaleUp.Core.Application.Integrations.Haravan.Features.Products;

public sealed class HaravanProductService(
    IHaravanProductHubApi haravanProductHubApi,
    MasterDataContext masterDataContext)
{
    public async Task SyncProducts()
    {
        var pendingProducts = await GetPendingProducts();
        if (!pendingProducts.Any()) return;
        
        var categories = await GetCategories();
        var vendors = await GetVendors();
        
        foreach (var batch in pendingProducts.Batch(100))
        {
            var productBatch = new List<Product>();
            foreach (var syncProduct in batch)
            {
                try
                {
                    var category = categories.FirstOrDefault(x => x.Id == syncProduct.CategoryId);
                    var vendor = vendors.FirstOrDefault(x => x.Id == syncProduct.VendorId);
                    
                    await DoSync(syncProduct, category, vendor);
                }
                catch (Exception ex)
                {
                    syncProduct.UpdateSyncStatus(ProductSyncStatus.Failed, ex.GetInnermostException().Message);
                }
                finally
                {
                    productBatch.Add(syncProduct);
                }
            }

            await UpdateSyncedProducts(productBatch);
        }
    }

    private async Task<List<Product>> GetPendingProducts()
    {
        return await masterDataContext.Products.Where(x => x.SyncStatus == ProductSyncStatus.Pending).ToListAsync();
    }
    
    private async Task<List<ProductCategory>> GetCategories()
    {
        return await masterDataContext.ProductCategories.ToListAsync();
    }
    
    private async Task<List<Vendor>> GetVendors()
    {
        return await masterDataContext.Vendors.ToListAsync();
    }

    private async Task DoSync(Product product, ProductCategory? category, Vendor? vendor)

    {
        var syncedProduct = await haravanProductHubApi.CreateProduct(new CreateHaravanProductRequest
        {
            Product = HaravanProductMapper.Map(product, category, vendor)
        });
        
        if (syncedProduct.Product.Id > 0)
        {
            product.UpdateSyncStatus(ProductSyncStatus.Synced);
            
            product.AddExternalId(PlatformType.Haravan, syncedProduct.Product.Id.ToString());
            
            foreach (var variant in product.Variants!)
            {
                var variantId = syncedProduct.Product.Variants
                    .FirstOrDefault(x => x.Barcode == variant.Code)?.Id;

                if (variantId != null)
                {
                    product.AddVariantExternalId(variant, PlatformType.Haravan, variantId!.Value.ToString());
                }
            }

            await SyncProductImages(product, syncedProduct);
        }
    }

    private async Task SyncProductImages(Product product, CreateHaravanProductResponse syncedProduct)
    {
        var variantImages = product.Variants!.Where(x => x.Images != null && x.Images.Any()).ToList();
        if (!variantImages.Any())
            return;
        
        foreach (var img in variantImages)
        {
            var variantId = syncedProduct.Product.Variants
                .FirstOrDefault(x => x.Barcode == img.Code)?.Id;

            var imageUrls = img.Images?.Select(x => x.Src).ToList();

            if (variantId == null || !imageUrls!.Any())
                continue;

            await CreateVariantImage(syncedProduct.Product.Id, variantId.Value, imageUrls, img);
        }
    }

    private async Task CreateVariantImage(long productId, long variantId, List<string>? imageUrls,
         ProductVariant variantImage)
    {
        foreach (var imageUrl in imageUrls!)
        {
            await haravanProductHubApi.CreateVariantImage(new CreateHaravanVariantImageRequest
            {
                ProductId = productId,
                Image = new HaravanVariantImageRequest
                {
                    VariantIds = [variantId],
                    Src = imageUrl,
                    Filename = $"{variantImage.Code}.jpg"
                }
            });
        }
    }

    private async Task UpdateSyncedProducts(List<Product> productBatch)
    {
        await masterDataContext.Products.AddRangeAsync(productBatch);
        await masterDataContext.SaveChangesAsync();
    }
}