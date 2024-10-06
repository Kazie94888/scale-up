using System.Globalization;
using ScaleUp.Core.Domain.Entities.AuditLogs;
using ScaleUp.Core.Domain.Entities.Orders;
using ScaleUp.Core.SharedKernel.Entities;

namespace ScaleUp.Core.Domain.Events.Orders;

public sealed class OrderConfirmedEvent : AuditEventBase
{
    public OrderConfirmedEvent(Guid orderId, string orderCode, DateTime confirmedAt, string previousOrderStatus, string note, UserInfo userInfo) : base(userInfo)
    {
        Parameters.Add(new AuditLogParameter(nameof(Order.Id), orderId.ToString()));
        Parameters.Add(new AuditLogParameter(nameof(Order.Code), orderCode));
        Parameters.Add(new AuditLogParameter(nameof(ConfirmedAt), confirmedAt.ToString(CultureInfo.InvariantCulture)));
        Parameters.Add(new AuditLogParameter(nameof(PreviousOrderStatus), previousOrderStatus));

        OrderCode = orderCode;
        OrderId = orderId;
        ConfirmedAt = confirmedAt;
        PreviousOrderStatus = previousOrderStatus;
        Note = note;
    }

    public string OrderCode { get; }
    public Guid OrderId { get; }
    public DateTime ConfirmedAt { get; }
    public string PreviousOrderStatus { get; }
    public string Note { get; }

    public override string GetDescription()
    {
        return $"Order {OrderCode} confirmed by {AuditedBy.Username}";
    }
}