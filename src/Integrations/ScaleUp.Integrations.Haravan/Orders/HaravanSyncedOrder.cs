using MongoDB.Bson.Serialization.Attributes;
using ScaleUp.Core.SharedKernel.Entities;
using ScaleUp.Core.SharedKernel.Enums;
using ScaleUp.Integrations.Haravan.Base.ValueObjects.Orders;

namespace ScaleUp.Integrations.Haravan.Orders;

public sealed class HaravanSyncedOrder : Entity
{
    public static HaravanSyncedOrder Create(HaravanOrder order, Guid tenantId)
    {
        var syncedOrder = new HaravanSyncedOrder
        {
            Id = Guid.NewGuid(),
            Payload = order,
            Status = HaravanSyncedOrderStatus.New,
            SyncedAt = DateTimeOffset.UtcNow,
            CreatedBy = UserInfo.Integration,
            TenantId = tenantId
        };

        return syncedOrder;
    }

    [BsonElement("_id")]
    public required Guid Id { get; set; }
    public required HaravanOrder Payload { get; set; }
    public required HaravanSyncedOrderStatus Status { get; set; }
    public required DateTimeOffset SyncedAt { get; set; }

    public Guid TenantId { get; set; }
}