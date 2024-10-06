using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Api.Base.Dtos;
using ScaleUp.Core.Persistence.Context;

namespace ScaleUp.Core.Api.Base.Middlewares;

public class TenantMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor, IServiceScopeFactory serviceScopeFactory)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var tenantId = await GetTenantId();
        if (tenantId == Guid.Empty)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("TenantId is not present or invalid.");
            return;
        }

        await next(context);
    }

    private async Task<Guid> GetTenantId()
    { 
        var httpContext = httpContextAccessor.HttpContext;
        var userInfo = UserInfoDto.BindAsync(httpContext!).Result;
        using var scope = serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ReadOnlyMasterDataContext>();
        var existedTenant = await dbContext.Tenants.AnyAsync(t => t.Id == userInfo.TenantId);
        
        if (userInfo.TenantId == Guid.Empty || !existedTenant)
        {
            return Guid.Empty;
        }
        
        return userInfo.TenantId;
    }
}