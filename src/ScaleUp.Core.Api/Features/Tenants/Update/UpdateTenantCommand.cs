using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using ScaleUp.Core.Api.Base.Dtos;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Tenants.Update;

internal sealed class UpdateTenantCommand : ICommand<UpdateTenantResponse>
{
    [FromRoute(Name = "tenantId")]
    public Guid TenantId { get; set; }

    [FromBody]
    public required UpdateTenantRequest Request { get; set; }

    public required UserInfoDto UserInfo { get; set; }
}

public sealed class UpdateTenantRequest
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("admin_email")]
    public required string AdminEmail { get; set; }
    
    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("version")]
    public required string Version { get; set; }

    [JsonPropertyName("activation_end_date")]
    public DateTime? ActivationEndDate { get; set; }

    [JsonPropertyName("activation_state")]
    public required string ActivationState { get; set; }
}

public sealed class UpdateTenantResponse
{
    public Guid TenantId { get; set; }
}