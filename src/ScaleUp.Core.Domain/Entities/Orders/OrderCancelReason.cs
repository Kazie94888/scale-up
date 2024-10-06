namespace ScaleUp.Core.Domain.Entities.Orders;

public sealed record OrderCancelReason(string Code, string? Description);