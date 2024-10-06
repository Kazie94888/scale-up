using ScaleUp.Core.Domain.Entities.AuditLogs;
using ScaleUp.Core.Domain.Entities.Orders;
using ScaleUp.Core.SharedKernel.Entities;

namespace ScaleUp.Core.Domain.Events.Orders;

public sealed class OrderSyncedEvent : AuditEventBase
{
    public OrderSyncedEvent(Order order, DateTime? confirmedAt, string? confirmedStatus, DateTime? cancelledAt, string? cancelReason, DateTime? closedAt, string? closedStatus, UserInfo createdBy) : base(createdBy)
    {
        Parameters.Add(new AuditLogParameter(nameof(Order.Id), order.Id.ToString()));
        Parameters.Add(new AuditLogParameter(nameof(Order.Code), order.Code));

        ConfirmedAt = confirmedAt;
        ConfirmedStatus = confirmedStatus;
        CancelledAt = cancelledAt;
        CancelReason = cancelReason;
        ClosedAt = closedAt;
        ClosedStatus = closedStatus;
        Order = order;
    }

    public DateTime? ConfirmedAt { get; }
    public string? ConfirmedStatus { get; }
    public DateTime? CancelledAt { get; set; }
    public string? CancelReason { get; set; }
    public DateTime? ClosedAt { get; set; }
    public string? ClosedStatus { get; set; }
    public Order Order { get; set; }

    public override string GetDescription()
    {
        return $"Order {Order.Code} synced by {AuditedBy.Username}";
    }
}