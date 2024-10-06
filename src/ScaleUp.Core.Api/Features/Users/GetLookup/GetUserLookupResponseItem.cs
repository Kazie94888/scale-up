using System.Text.Json.Serialization;

namespace ScaleUp.Core.Api.Features.Users.GetLookup;

internal sealed record GetUserLookupResponseItem
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }


    [JsonPropertyName("email")]
    public required string Email { get; set; }

    [JsonPropertyName("status")]
    public required string Status { get; set; }
}