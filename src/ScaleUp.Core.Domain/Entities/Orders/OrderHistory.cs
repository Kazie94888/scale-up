namespace ScaleUp.Core.Domain.Entities.Orders;

public sealed record OrderHistory
{
    private OrderHistory()
    {
    }

    public OrderHistory(string? orderCode, string? orderStatus,
        DateTime createdAt, string? createdBy, string? action,
        OrderHistoryDescription? description, string? note)
    {
        OrderCode = orderCode;
        OrderStatus = orderStatus;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        Action = action;
        Description = description;
        Note = note;
    }

    public string? OrderCode { get; set; }
    public string? OrderStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? Action { get; set; }
    public OrderHistoryDescription? Description { get; set; }
    public string? Note { get; set; }
}

public sealed record OrderHistoryDescription
{
    public string? PreviousStatus { get; set; }
    public OrderAssignee? Assignee { get; set; }
    public string? UsernameAssign { get; set; }
    public string? CurrentOrderDelivery { get; set; }
    public string? PreviousOrderDelivery { get; set; }
    public string? CurrentOrderWarehouse { get; set; }
    public string? CurrentPaymentMethod { get; set; }
    public string? PreviousPaymentMethod { get; set; }
    public string? CancelReasonCode { get; set; }

    private OrderHistoryDescription()
    {

    }
    public OrderHistoryDescription(
    string? previousStatus,
    OrderAssignee? assignee,
    string? usernameAssign,
    string? currentOrderDelivery,
    string? previousOrderDelivery,
    string? currentOrderWarehouse,
    string? currentPaymentMethod,
    string? previousPaymentMethod,
    string? cancelReasonCode)
    {
        PreviousStatus = previousStatus;
        Assignee = assignee;
        UsernameAssign = usernameAssign;
        CurrentOrderDelivery = currentOrderDelivery;
        PreviousOrderDelivery = previousOrderDelivery;
        CurrentOrderWarehouse = currentOrderWarehouse;
        CurrentPaymentMethod = currentPaymentMethod;
        PreviousPaymentMethod = previousPaymentMethod;
        CancelReasonCode = cancelReasonCode;
    }
}