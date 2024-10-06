using MongoDB.Bson.Serialization.Attributes;
using ScaleUp.Core.Domain.Base.Interfaces;
using ScaleUp.Core.Domain.Enums;
using ScaleUp.Core.Domain.Events.Orders;
using ScaleUp.Core.Domain.Events.Orders.Payments;
using ScaleUp.Core.SharedKernel.Entities;
using ScaleUp.Core.SharedKernel.Extensions;

namespace ScaleUp.Core.Domain.Entities.Orders;

public sealed class Order : AggregateRoot, IMultiTenant
{
    //TODO: Add Fulfillment and ExternalUpdatedAt,GroupRelativeId, RelativeOrders
    private Order()
    {
    }

    public static Order Create(string code,
        string externalId,
        string externalCode,
        string source,
        string channel,
        string status,
        OrderCustomer customer,
        List<OrderLineItem> lines,
        decimal subTotal,
        decimal discountAmount,
        string financialStatus,
        decimal shippingFee,
        decimal taxPercent,
        decimal taxAmount,
        decimal totalBeforeTax,
        decimal total,
        string? refPromotion,
        OrderAssignee? assignee,
        OrderDelivery delivery,
        List<OrderPayment> payments,
        OrderCancelReason? cancelReason,
        OrderWarehouse? warehouse,
        List<OrderFulfillment> fulfillments,
        UserInfo createdBy,
        DateTime externalCreatedAt,
        Guid? tenantId = null)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            Code = code,
            Status = status,
            ExternalId = externalId,
            ExternalCode = externalCode,
            Source = source,
            Channel = channel,
            Customer = customer,
            Lines = lines,
            SubTotal = subTotal,
            DiscountAmount = discountAmount,
            FinancialStatus = financialStatus,
            ShippingFee = shippingFee,
            TaxPercent = taxPercent,
            TaxAmount = taxAmount,
            TotalBeforeTax = totalBeforeTax,
            Total = total,
            RefPromotion = refPromotion,
            Assignee = assignee,
            AssigneeId = assignee?.Id,
            Delivery = delivery,
            Payments = payments,
            CancelReason = cancelReason,
            Warehouse = warehouse,
            Fulfillments = fulfillments,
            CreatedBy = createdBy,
            ExternalCreatedAt = externalCreatedAt,
        };

        if (tenantId != null)
            order.TenantId = tenantId.Value;

        order.AddDomainEvent(new OrderCreatedEvent(order, createdBy));

        return order;
    }

    [BsonElement("_id")]
    public required Guid Id { get; set; }

    public required string Code { get; set; }
    public required string ExternalId { get; set; }
    public required string ExternalCode { get; set; }
    public string? Source { get; set; }
    public required string Channel { get; set; }
    public required string Status { get; set; }
    public required OrderCustomer Customer { get; set; }
    public List<OrderLineItem> Lines { get; set; }
    public required decimal Total { get; set; }
    public decimal? SubTotal { get; set; }
    public decimal? DiscountAmount { get; set; }
    public string? FinancialStatus { get; set; }
    public decimal? ShippingFee { get; set; }
    public decimal? TaxPercent { get; set; }
    public decimal? TaxAmount { get; set; }
    public decimal? TotalBeforeTax { get; set; }
    public string? RefPromotion { get; set; }
    public string? Note { get; set; }
    public Guid? AssigneeId { get; set; }
    public OrderAssignee? Assignee { get; set; }
    public required OrderDelivery Delivery { get; set; }
    public required List<OrderPayment> Payments { get; set; }
    public List<OrderFulfillment>? Fulfillments { get; set; }
    public string? CancelReasonCode { get; set; }
    public OrderCancelReason? CancelReason { get; set; }
    public required OrderWarehouse Warehouse { get; set; }

    public DateTime ExternalCreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    [BsonIgnore]
    public IReadOnlyList<OrderHistory> History => _history;

    [BsonElement("history")]
    internal List<OrderHistory> _history { get; set; }
    public Guid TenantId { get; set; }

    public void Confirm(string note, UserInfo confirmedBy)
    {
        var previousStatus = Status;
        Status = OrderStatus.Confirmed;

        AddDomainEvent(new OrderConfirmedEvent(orderId: Id, orderCode: Code, confirmedAt: DateTime.UtcNow, previousOrderStatus: previousStatus, note: note, userInfo: confirmedBy));
    }

    public void UpdateStatus(string newOrderStatus, string note, UserInfo confirmedBy)
    {
        var previousStatus = Status;
        Status = newOrderStatus;

        AddDomainEvent(new OrderStatusUpdatedEvent(orderId: Id, orderCode: Code, updatedAt: DateTime.UtcNow, previousStatus, newOrderStatus, note: note, userInfo: confirmedBy));
    }

    public void Cancel(string cancelReasonCode, string? cancelReasonDescription, string note, UserInfo cancelledBy)
    {
        CancelReason = new OrderCancelReason(cancelReasonCode, cancelReasonDescription);
        CancelReasonCode = cancelReasonCode;

        var previousStatus = Status;
        Status = OrderStatus.Cancelled;

        AddDomainEvent(new OrderCancelledEvent(Id, Code, DateTime.UtcNow, previousStatus, note, cancelledBy));
    }

    public void AddHistory(OrderHistory history)
    {
        _history ??= [];
        _history.Add(history);
    }

    public void Pick(string note, UserInfo pickedBy)
    {
        var previousStatus = Status;
        Status = OrderStatus.Picked;

        AddDomainEvent(new OrderPickedEvent(orderId: Id, orderCode: Code, pickedAt: DateTime.UtcNow, previousOrderStatus: previousStatus, note: note, userInfo: pickedBy));
    }

    public void Pack(string note, UserInfo packedBy)
    {
        var previousStatus = Status;
        Status = OrderStatus.Packed;

        AddDomainEvent(new OrderPackedEvent(Id, Code, DateTime.UtcNow, previousStatus, note, packedBy));
    }

    public void ConfirmPayment(string previousFinancialStatus, decimal amount, UserInfo confirmedBy)
    {
        foreach (var payment in Payments!)
        {
            payment.PaymentStatus = OrderFinancialStatus.Paid.GetDescription();
        }
        FinancialStatus = OrderFinancialStatus.Paid.GetDescription();

        AddDomainEvent(new OrderPaymentConfirmedEvent(Id, Code, DateTime.UtcNow, amount, previousFinancialStatus, FinancialStatus, confirmedBy));
    }

    public void Deliver(string note, UserInfo deliveredBy)
    {
        var previousStatus = Status;
        Status = OrderStatus.DeliveryPicking;

        AddDomainEvent(new OrderDeliveredEvent(Id, Code, DateTime.UtcNow, previousStatus, note, deliveredBy));
    }

    public void AddFulfillment(OrderFulfillment fulfillment)
    {
        Fulfillments ??= [];
        Fulfillments.Add(fulfillment);
    }

    public void Sync(DateTime? confirmedAt, string? confirmedStatus, DateTime? cancelledAt, string? cancelReason,
        DateTime? closedAt, string? closedStatus, UserInfo syncedBy)
    {
        AddDomainEvent(new OrderSyncedEvent(this, confirmedAt, confirmedStatus, cancelledAt, cancelReason, closedAt, closedStatus, syncedBy));
    }

    public void UpdateDelivery(UserInfo updatedBy)
    {
        AddDomainEvent(new OrderDeliveryUpdatedEvent(Id, Code, DateTime.UtcNow, updatedBy));
    }
}
