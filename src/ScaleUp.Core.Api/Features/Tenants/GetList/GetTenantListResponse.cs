using System.Text.Json.Serialization;

namespace ScaleUp.Core.Api.Features.Tenants.GetList;

internal sealed class GetTenantListResponse
{
    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("data")]
    public IEnumerable<GetTenantsResponseItem>? Data { get; set; }
}

internal sealed class GetTenantsResponseItem
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("admin_email")]
    public required string AdminEmail { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("version")]
    public required string Version { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("activation_end_date")]
    public DateTime? ActivationEndDate { get; set; }

    [JsonPropertyName("activation_state")]
    public required string ActivationState { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }
}