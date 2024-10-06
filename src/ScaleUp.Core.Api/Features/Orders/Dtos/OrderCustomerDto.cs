using System.Text.Json.Serialization;

namespace ScaleUp.Core.Api.Features.Orders.Dtos
{
    internal sealed record OrderCustomerDto
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        //TODO: consider Profiles and Code if needed
        [JsonPropertyName("profiles")]
        public List<OrderCustomerProfileDto>? Profiles { get; set; }
    }
}
