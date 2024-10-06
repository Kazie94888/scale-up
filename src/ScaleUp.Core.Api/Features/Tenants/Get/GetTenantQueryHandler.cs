using FluentResults;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Tenants.Get;

internal sealed class GetTenantQueryHandler(ReadOnlyMasterDataContext dataContext)
    : IQueryHandler<GetTenantQuery, GetTenantResponse>
{
    public async Task<Result<GetTenantResponse>> Handle(GetTenantQuery request, CancellationToken cancellationToken)
    {
        var tenant = await dataContext.Tenants.Where(t => t.Id == request.TenantId)
                                              .Select(tenant => new GetTenantResponse
                                              {
                                                  Id = tenant.Id,
                                                  Name = tenant.Name,
                                                  AdminEmail = tenant.AdminEmail,
                                                  Phone = tenant.Phone,
                                                  Version = tenant.Version,
                                                  ActivationState = tenant.ActivationState,
                                                  UpdatedAt = tenant.UpdatedAt,
                                                  ActivationEndDate = tenant.ActivationEndDate,
                                                  CreatedAt = tenant.CreatedAt,
                                                  CreatedBy = new Base.Dtos.UserInfoDto
                                                  {
                                                      Id = tenant.CreatedBy.Id,
                                                      Username = tenant.CreatedBy.Username,
                                                      Type = tenant.CreatedBy.Type,
                                                      TenantId = tenant.CreatedBy.Id
                                                  }
                                              })
                                              .FirstAsync(cancellationToken);
        return Result.Ok(tenant);
    }
}