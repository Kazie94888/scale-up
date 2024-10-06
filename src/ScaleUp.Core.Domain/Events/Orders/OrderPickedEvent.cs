using System.Globalization;
using ScaleUp.Core.Domain.Entities.AuditLogs;
using ScaleUp.Core.Domain.Entities.Orders;
using ScaleUp.Core.SharedKernel.Entities;

namespace ScaleUp.Core.Domain.Events.Orders;

public sealed class OrderPickedEvent : AuditEventBase
{
    public OrderPickedEvent(Guid orderId, string orderCode, DateTime pickedAt, string previousOrderStatus, string note, UserInfo userInfo) : base(userInfo)
    {
        Parameters.Add(new AuditLogParameter(nameof(Order.Id), orderId.ToString()));
        Parameters.Add(new AuditLogParameter(nameof(Order.Code), orderCode));
        Parameters.Add(new AuditLogParameter(nameof(PickedAt), pickedAt.ToString(CultureInfo.InvariantCulture)));
        Parameters.Add(new AuditLogParameter(nameof(PreviousOrderStatus), previousOrderStatus));

        OrderCode = orderCode;
        OrderId = orderId;
        PickedAt = pickedAt;
        PreviousOrderStatus = previousOrderStatus;
        Note = note;
    }
    public string OrderCode { get; }
    public Guid OrderId { get; }
    public DateTime PickedAt { get; }
    public string PreviousOrderStatus { get; }
    public string Note { get; }

    public override string GetDescription()
    {
        return $"Order {OrderCode} picked by {AuditedBy.Username}";
    }
}