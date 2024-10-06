using ScaleUp.Core.Domain.Entities.AuditLogs;
using ScaleUp.Core.Domain.Entities.Orders;
using ScaleUp.Core.SharedKernel.Entities;
using System.Globalization;

namespace ScaleUp.Core.Domain.Events.Orders.Payments;

public sealed class OrderPaymentConfirmedEvent : AuditEventBase
{
    public OrderPaymentConfirmedEvent(Guid orderId, string orderCode, DateTime confirmedAt, decimal amount, string previousFinancialStatus, string newFinancialStatus, UserInfo userInfo) : base(userInfo)
    {
        Parameters.Add(new AuditLogParameter(nameof(Order.Id), orderId.ToString()));
        Parameters.Add(new AuditLogParameter(nameof(Order.Code), orderCode));
        Parameters.Add(new AuditLogParameter(nameof(ConfirmedAt), confirmedAt.ToString(CultureInfo.InvariantCulture)));
        Parameters.Add(new AuditLogParameter(nameof(Amount), amount.ToString()));
        Parameters.Add(new AuditLogParameter(nameof(PreviousFinancialStatus), previousFinancialStatus));
        Parameters.Add(new AuditLogParameter(nameof(NewFinancialStatus), newFinancialStatus));

        OrderCode = orderCode;
        OrderId = orderId;
        ConfirmedAt = confirmedAt;
        PreviousFinancialStatus = previousFinancialStatus;
        NewFinancialStatus = newFinancialStatus;
        Amount = amount;
    }

    public string OrderCode { get; }
    public Guid OrderId { get; }
    public DateTime ConfirmedAt { get; }
    public decimal Amount { get; }
    public string PreviousFinancialStatus { get; }
    public string NewFinancialStatus { get; }

    public override string GetDescription()
    {
        return $"Order {OrderCode} payment confirmed by {AuditedBy.Username}.";
    }
}