using ScaleUp.Core.Domain.Entities.AuditLogs;
using ScaleUp.Core.Domain.Entities.Orders;
using ScaleUp.Core.SharedKernel.Entities;
using System.Globalization;

namespace ScaleUp.Core.Domain.Events.Orders;

public sealed class OrderStatusUpdatedEvent : AuditEventBase
{
    public OrderStatusUpdatedEvent(Guid orderId, string orderCode, DateTime updatedAt, string previousOrderStatus, string newOrderStatus, string note, UserInfo userInfo) : base(userInfo)
    {
        Parameters.Add(new AuditLogParameter(nameof(Order.Id), orderId.ToString()));
        Parameters.Add(new AuditLogParameter(nameof(Order.Code), orderCode));
        Parameters.Add(new AuditLogParameter(nameof(UpdatedAt), updatedAt.ToString(CultureInfo.InvariantCulture)));
        Parameters.Add(new AuditLogParameter(nameof(PreviousOrderStatus), previousOrderStatus));
        Parameters.Add(new AuditLogParameter(nameof(NewOrderStatus), newOrderStatus));

        OrderCode = orderCode;
        OrderId = orderId;
        UpdatedAt = updatedAt;
        PreviousOrderStatus = previousOrderStatus;
        NewOrderStatus = newOrderStatus;
        Note = note;
    }

    public string OrderCode { get; }
    public Guid OrderId { get; }
    public DateTime UpdatedAt { get; }
    public string PreviousOrderStatus { get; }
    public string NewOrderStatus { get; }
    public string Note { get; }

    public override string GetDescription()
    {
        return $"Order {OrderCode} changed status from {PreviousOrderStatus} to {NewOrderStatus} by {AuditedBy.Username}.";
    }
}