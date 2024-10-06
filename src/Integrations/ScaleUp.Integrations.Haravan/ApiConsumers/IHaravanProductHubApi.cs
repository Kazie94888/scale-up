using Refit;
using ScaleUp.Integrations.Haravan.Products.Create;
using ScaleUp.Integrations.Haravan.Products.GetList;
using ScaleUp.Integrations.Haravan.ProductVariantImages.Create;

namespace ScaleUp.Integrations.Haravan.ApiConsumers;

public interface IHaravanProductHubApi
{
    [Get("/com/products.json")]
    Task<GetHaravanProductResponse> GetProducts([Body] GetHaravanProductRequest request);
    
    [Post("/com/products.json")]
    Task<CreateHaravanProductResponse> CreateProduct([Body] CreateHaravanProductRequest request);
    
    [Post("/com/products/{request.productId}/images.json")]
    Task<CreateHaravanVariantImageResponse> CreateVariantImage([Body] CreateHaravanVariantImageRequest request);
}