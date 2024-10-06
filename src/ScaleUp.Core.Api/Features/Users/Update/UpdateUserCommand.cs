using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using ScaleUp.Core.Api.Base.Dtos;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Users.Update;

internal sealed class UpdateUserCommand : ICommand<UpdateUserResponse>
{
    [FromRoute(Name ="userId")]
    public Guid UserId { get; set; }
    
    [FromBody]
    public required UpdateUserRequest Request { get; set; }
    public required UserInfoDto UserInfo { get; set; }
}

internal sealed class UpdateUserResponse
{
    public required Guid UserId { get; set; }
}

internal sealed class UpdateUserRequest
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