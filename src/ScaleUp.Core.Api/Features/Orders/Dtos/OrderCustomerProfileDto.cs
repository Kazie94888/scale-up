using System.Text.Json.Serialization;

namespace ScaleUp.Core.Api.Features.Orders.Dtos;

internal sealed record OrderCustomerProfileDto
{
    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("phone_number")]
    public string? PhoneNumber { get; set; }
}
