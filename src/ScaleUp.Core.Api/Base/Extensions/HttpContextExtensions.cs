using ScaleUp.Core.Api.Base.Dtos;
using ScaleUp.Core.SharedKernel.Constants;
using ScaleUp.Core.SharedKernel.Entities;
using ScaleUp.Core.SharedKernel.Enums;
using ScaleUp.Core.SharedKernel.Extensions;

namespace ScaleUp.Core.Api.Base.Extensions;

public static class HttpContextExtensions
{
    private const string _userIdClaim = "http://schemas.microsoft.com/identity/claims/objectidentifier";

    // TODO: find a better way to get user principal from context
    public static UserInfoDto GetUserInfo(this HttpContext context)
    {
        var userIdClaim = context.User.FindFirst(_userIdClaim)?.Value;
        var tenantIdClaim = context.User.FindFirst(ClaimTypeConstants.TenantId)?.Value;
        var username = context.User.Identity?.Name;

        var userId = userIdClaim.IsBlank() ? Guid.NewGuid() : new Guid(userIdClaim);
        var tenantId = tenantIdClaim.IsBlank() ? Guid.NewGuid() : new Guid(tenantIdClaim);

        return new UserInfoDto
        {
            Id = userId,
            Username = username ?? UserInfo.System.Username,
            Type = UserInfoType.BackOffice,
            TenantId = tenantId
        };
    }
}