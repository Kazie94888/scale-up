using System.Text.Json.Serialization;

namespace ScaleUp.Core.Api.Features.Roles.GetList;

internal sealed class GetRoleListResponse
{
    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("data")]
    public IEnumerable<GetRolesResponseItem>? Data { get; set; }
}

internal sealed class GetRolesResponseItem
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("created_by")]
    public required string CreatedBy { get; set; }

    [JsonPropertyName("member_count")]
    public int MemberCount { get; set; }

    [JsonPropertyName("permission_count")]
    public int PermissionCount { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("status")]
    public required string Status { get; set; }
}