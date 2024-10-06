using System.Text.Json.Serialization;
using ScaleUp.Core.Api.Features.Users.Dtos;

namespace ScaleUp.Core.Api.Features.Users.Get;

internal sealed record GetUserResponse
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("first_name")]
    public required string FirstName { get; set; }
    
    [JsonPropertyName("last_name")]
    public required string LastName { get; set; }

    [JsonPropertyName("email")]
    public required string Email { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("roles")]
    public required IEnumerable<UserRoleDto> Roles { get; set; }

    [JsonPropertyName("warehouses")]
    public required IEnumerable<UserWarehouseDto> Warehouses { get; set; }

    [JsonPropertyName("status")]
    public required string Status { get; set; }
}