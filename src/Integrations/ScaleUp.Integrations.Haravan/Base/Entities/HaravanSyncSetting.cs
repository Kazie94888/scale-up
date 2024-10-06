using MongoDB.Bson.Serialization.Attributes;
using ScaleUp.Core.SharedKernel.Entities;

namespace ScaleUp.Integrations.Haravan.Base.Entities;

public class HaravanSyncSetting : Entity
{
    [BsonElement("_id")]
    public required Guid Id { get; set; }

    public DateTimeOffset OrderSyncedAt { get; set; }
    public string OrderSyncError { get; set; }

    public DateTimeOffset ProductSyncedAt { get; set; }
}