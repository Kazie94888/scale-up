using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using ScaleUp.Core.Api.Base.Dtos;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Users.Create;

public sealed record CreateUserCommand : ICommand<CreateUserResponse>
{
    [FromBody]
    public required CreateUserRequest User { get; set; }

    public required UserInfoDto UserInfo { get; set; }
}

public sealed record CreateUserRequest
{
    [JsonPropertyName("first_name")]
    public required string FirstName { get; set; }
    
    [JsonPropertyName("last_name")]
    public required string LastName { get; set; }
    
    [JsonPropertyName("email")]
    public required string Email { get; set; }
    
    [JsonPropertyName("phone")]
    public string? Phone { get; set; }
    
    [JsonPropertyName("status")]
    public required string Status { get; set; }
    
    [JsonPropertyName("role_ids")]
    public required List<Guid> RoleIds { get; set; }
    
    [JsonPropertyName("warehouse_ids")]
    public required List<Guid> WarehouseIds { get; set; }
}

public sealed record CreateUserResponse
{
    public Guid EntityId { get; set; }
}