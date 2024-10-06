namespace ScaleUp.Core.Domain.Entities.Orders;

public sealed class OrderPayment(
    string? orderCode,
    string? paymentCode,
    decimal amount,
    string? paymentMethod,
    string? paymentGateway,
    string? paymentStatus,
    string? addressLine1,
    string? addressLine2,
    string? ward,
    string? district,
    string? city,
    string? country,
    DateTime createdAt)
{
    public string? OrderCode { get; set; } = orderCode;
    public string? PaymentCode { get; set; } = paymentCode;
    public decimal Amount { get; set; } = amount;
    public string? PaymentMethod { get; set; } = paymentMethod;
    public string? PaymentGateway { get; set; } = paymentGateway;
    public string? PaymentStatus { get; set; } = paymentStatus;
    public string? AddressLine1 { get; set; } = addressLine1;
    public string? AddressLine2 { get; set; } = addressLine2;
    public string? Ward { get; set; } = ward;
    public string? District { get; set; } = district;
    public string? City { get; set; } = city;
    public string? Country { get; set; } = country;
    public DateTime CreatedAt { get; set; } = createdAt;
}