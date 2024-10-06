using System.Globalization;
using ScaleUp.Core.Domain.Entities.AuditLogs;
using ScaleUp.Core.Domain.Entities.Orders;
using ScaleUp.Core.SharedKernel.Entities;

namespace ScaleUp.Core.Domain.Events.Orders;

public class OrderDeliveryUpdatedEvent : AuditEventBase
{
    public OrderDeliveryUpdatedEvent(Guid orderId, string orderCode, DateTime updatedAt, UserInfo userInfo) : base(userInfo)
    {
        Parameters.Add(new AuditLogParameter(nameof(Order.Id), orderId.ToString()));
        Parameters.Add(new AuditLogParameter(nameof(Order.Code), orderCode));
        Parameters.Add(new AuditLogParameter(nameof(UpdatedAt), updatedAt.ToString(CultureInfo.InvariantCulture)));

        OrderCode = orderCode;
        OrderId = orderId;
        UpdatedAt = updatedAt;
    }
    
    public string OrderCode { get; }
    public Guid OrderId { get; }
    public DateTime UpdatedAt { get; }


    public override string GetDescription()
    {
        return $"Order {OrderCode} delivery updated by {AuditedBy.Username}";
    }
}