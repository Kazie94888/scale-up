using ScaleUp.Core.Domain.Entities.AuditLogs;
using ScaleUp.Core.Domain.Entities.Orders;
using ScaleUp.Core.SharedKernel.Entities;

namespace ScaleUp.Core.Domain.Events.Orders;

public sealed class OrderCreatedEvent : AuditEventBase
{
    public OrderCreatedEvent(Order order, UserInfo userInfo) : base(userInfo)
    {
        Parameters.Add(new AuditLogParameter(nameof(Order.Id), order.Id.ToString()));
        Parameters.Add(new AuditLogParameter(nameof(Order.Code), order.Code));

        Order = order;
    }

    public Order Order { get; }

    public override string GetDescription()
    {
        return $"Order {Order.Code} created by {AuditedBy.Username}";
    }
}