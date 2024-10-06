using MongoDB.Bson.Serialization.Attributes;
using ScaleUp.Core.SharedKernel.Entities;

namespace ScaleUp.Core.Domain.Entities.Products;

public sealed class ProductCategory : AggregateRoot
{
    private ProductCategory()
    {
    }

    [BsonElement("_id")] 
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int? ParentId { get; set; }
} 