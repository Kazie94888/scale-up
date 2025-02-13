﻿using ScaleUp.Core.Domain.Entities.AuditLogs;
using ScaleUp.Core.Domain.Entities.Orders;
using ScaleUp.Core.SharedKernel.Entities;

namespace ScaleUp.Core.Domain.Events.Orders;

public sealed class OrderCancelledEvent : AuditEventBase
{
    public OrderCancelledEvent(Guid orderId, string orderCode, DateTime cancelledAt, string previousOrderStatus, string note, UserInfo userInfo) : base(userInfo)
    {
        Parameters.Add(new AuditLogParameter(nameof(Order.Id), orderId.ToString()));
        Parameters.Add(new AuditLogParameter(nameof(Order.Code), orderCode));
        Parameters.Add(new AuditLogParameter(nameof(CancelledAt), cancelledAt.ToString()));
        Parameters.Add(new AuditLogParameter(nameof(PreviousOrderStatus), previousOrderStatus));

        OrderCode = orderCode;
        OrderId = orderId;
        CancelledAt = cancelledAt;
        PreviousOrderStatus = previousOrderStatus;
        Note = note;
    }

    public string OrderCode { get; }
    public Guid OrderId { get; }
    public DateTime CancelledAt { get; }
    public string PreviousOrderStatus { get; }
    public string Note { get; }

    public override string GetDescription()
    {
        return $"Order {OrderCode} cancelled by {AuditedBy.Username}";
    }
}