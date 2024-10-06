using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using ScaleUp.Core.Api.Base.Dtos;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Roles.Create;

internal sealed class CreateRoleCommand : ICommand<CreateRoleResponse>
{
    [FromBody]
    public required CreateRoleRequest Request { get; set; }

    public required UserInfoDto UserInfo { get; set; }
}

internal sealed class CreateRoleResponse
{
    [JsonPropertyName("role_id")]
    public Guid RoleId { get; set; }
}

internal sealed class CreateRoleRequest
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("permission_ids")]
    public required List<string> PermissionIds { get; set; }

    [JsonPropertyName("user_ids")]
    public required List<Guid> UserIds { get; set; }

    [JsonPropertyName("status")]
    public required string Status { get; set; }
}