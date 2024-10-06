using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using ScaleUp.Core.Api.Base.Dtos;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Roles.Update;

internal sealed class UpdateRoleCommand : ICommand<UpdateRoleResponse>
{
    [FromRoute(Name = "roleId")]
    public Guid RoleId { get; set; }
    
    [FromBody]
    public required UpdateRoleRequest Request { get; set; }
    public required UserInfoDto UserInfo { get; set; }
}

internal sealed class UpdateRoleResponse
{
    [JsonPropertyName("role_id")]
    public Guid RoleId { get; set; }
}

internal sealed class UpdateRoleRequest
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    
    [JsonPropertyName("permission_ids")]
    public required List<string> Permissions { get; set; }

    [JsonPropertyName("user_ids")]
    public required List<Guid> UserIds { get; set; }

    [JsonPropertyName("status")]
    public required string Status { get; set; }
}