namespace ScaleUp.Core.Domain.Entities.Orders;

public sealed record OrderCustomer(string? FirstName, string? LastName, string? Email, string? PhoneNumber);
