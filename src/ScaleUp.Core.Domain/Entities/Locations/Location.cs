using MongoDB.Bson.Serialization.Attributes;
using ScaleUp.Core.SharedKernel.Entities;

namespace ScaleUp.Core.Domain.Entities.Locations;


public sealed class Location : AggregateRoot
{
    [BsonElement("_id")]
    public required Guid Id { get; set; }
    public required string Code { get; set; }
    public required int ExternalId { get; set; }
    public required string Type { get; set; }
    public required string Name { get; set; }
    public Guid? ParentId { get; set; }
}