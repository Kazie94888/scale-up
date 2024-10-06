using FluentResults;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Tenants.GetList;

internal sealed class GetTenantListQueryHandler(ReadOnlyMasterDataContext dataContext)
    : IQueryHandler<GetTenantListQuery, GetTenantListResponse>
{
    public async Task<Result<GetTenantListResponse>> Handle(GetTenantListQuery query, CancellationToken cancellationToken)
    {
        var data = await dataContext.Tenants.OrderByDescending(t => t.CreatedAt).Skip(query.Page * query.PageSize).Take(query.PageSize).Select(t =>
            new GetTenantsResponseItem
            {
                Id = t.Id,
                Name = t.Name,
                AdminEmail = t.AdminEmail,
                Phone = t.Phone,
                Version = t.Version,
                ActivationState = t.ActivationState,
                UpdatedAt = t.UpdatedAt,
                ActivationEndDate = t.ActivationEndDate,
                CreatedAt = t.CreatedAt
            }).ToListAsync(cancellationToken);

        var total = await dataContext.Tenants.CountAsync(cancellationToken);

        return Result.Ok(new GetTenantListResponse
        {
            Total = total,
            Data = data
        });
    }
}