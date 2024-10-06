using MongoDB.Bson.Serialization.Attributes;
using ScaleUp.Core.Domain.Enums;
using ScaleUp.Core.SharedKernel.Entities;

namespace ScaleUp.Core.Domain.Entities.Products;

public sealed class Product : AggregateRoot
{
    private Product()
    {
    }
    
    public static Product Create(string name, string code, decimal price, string status, string slug, Guid categoryId, Guid vendorId, UserInfo createdBy)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Code = code,
            Price = price,
            Status = status,
            Slug = slug,
            CategoryId = categoryId,
            VendorId = vendorId,
            SyncStatus = ProductSyncStatus.Hold,
            CreatedBy = createdBy
        };

        // product.AddDomainEvent(new ProductCreatedEvent(order.Id, code, createdBy));

        return product;
    }

    [BsonElement("_id")] 
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; }
    public required decimal Price { get; set; }
    public decimal? OfferPrice { get; set; }
    public string? Description { get; set; }
    public required string Status { get; set; }
    public required string Slug { get; set; }
    public required Guid CategoryId { get; set; }
    public string? Tags { get; set; }
    public required Guid VendorId { get; set; }

    public List<ProductVariant>? Variants { get; set; }
    public List<ProductImage>? Images { get; set; }
    
    public List<ExternalId>? ExternalIds { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public required ProductSyncStatus SyncStatus { get; set; }
    public string? SyncMessage { get; set; }
    
    
    public void UpdateSyncStatus(ProductSyncStatus syncStatus, string? message = null)
    {
        SyncStatus = syncStatus;
        SyncMessage = message;
    }
    
    public void AddExternalId(PlatformType sourceType, string id)
    {
        ExternalIds ??= [];
        ExternalIds.Add(new ExternalId(sourceType, id));
    }
    
    public void AddVariantExternalId(ProductVariant variant, PlatformType sourceType, string id)
    {
        variant.ExternalIds ??= [];
        variant.ExternalIds.Add(new ExternalId(sourceType, id));
    }
}