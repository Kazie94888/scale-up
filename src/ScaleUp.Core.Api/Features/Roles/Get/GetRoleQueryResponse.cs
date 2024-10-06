using System.Text.Json.Serialization;
using ScaleUp.Core.Api.Base.Dtos;

namespace ScaleUp.Core.Api.Features.Roles.Get;

internal sealed record GetRoleQueryResponse
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("permission_ids")]
    public required List<string> PermissionIds { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }
    
    [JsonPropertyName("status")]
    public required string Status { get; set; }
    
    [JsonPropertyName("type")]
    public required string Type { get; set; }
    
    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("created_by")]
    public required string CreatedBy { get; init; }

    [JsonPropertyName("users")]
    public required List<RoleUserDto> Users { get; init; }
}

internal sealed record RoleUserDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}