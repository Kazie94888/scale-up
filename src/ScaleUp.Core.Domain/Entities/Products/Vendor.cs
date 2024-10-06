using MongoDB.Bson.Serialization.Attributes;
using ScaleUp.Core.SharedKernel.Entities;

namespace ScaleUp.Core.Domain.Entities.Products;

public sealed class Vendor : AggregateRoot
{
    private Vendor()
    {
    }

    [BsonElement("_id")]
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}