using System.Text.Json.Serialization;

namespace ScaleUp.Core.Api.Features.Orders.Dtos;

internal sealed record OrderPaymentDto
{
    [JsonPropertyName("amount")]
    public required decimal Amount { get; set; }

    [JsonPropertyName("payment_status")]
    public required string PaymentStatus { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("payment_code")]
    public string? PaymentCode { get; set; }

    [JsonPropertyName("payment_method")]
    public string? PaymentMethod { get; set; }

    [JsonPropertyName("payment_gateway")]
    public string? PaymentGateway { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}
