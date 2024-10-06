using ScaleUp.Core.Api.Base.Extensions;
using ScaleUp.Core.SharedKernel.Entities;
using ScaleUp.Core.SharedKernel.Enums;

namespace ScaleUp.Core.Api.Base.Dtos;

public sealed record UserInfoDto
{
    public required Guid Id { get; set; }
    public required string Username { get; set; }
    public required UserInfoType Type { get; set; }
    public Guid TenantId { get; set; }

    public static implicit operator UserInfoDto(UserInfo userInfo) => new()
    {
        Id = userInfo.Id,
        Username = userInfo.Username,
        Type = userInfo.Type,
        TenantId = Guid.Empty,
    };

    public static implicit operator UserInfo(UserInfoDto userInfo) => new()
    {
        Id = userInfo.Id,
        Username = userInfo.Username,
        Type = userInfo.Type,
    };

    public static ValueTask<UserInfoDto> BindAsync(HttpContext context)
    {
        return ValueTask.FromResult(context.GetUserInfo());
    }
}