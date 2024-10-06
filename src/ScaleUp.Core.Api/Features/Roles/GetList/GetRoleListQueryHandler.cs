using FluentResults;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Domain.Entities.Roles;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Roles.GetList;

internal class GetRoleListQueryHandler(ReadOnlyMasterDataContext dataContext)
    : IQueryHandler<GetRoleListQuery, GetRoleListResponse>
{
    public async Task<Result<GetRoleListResponse>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Role> roleQuery = dataContext.Roles;
        
        var total = await roleQuery.CountAsync(cancellationToken);
        var roles = await roleQuery.OrderByDescending(r => r.CreatedAt).Skip(request.Page * request.PageSize).Take(request.PageSize)
            .ToListAsync(cancellationToken);
        
        var roleIds = roles.Select(x => x.Id);

        var users = await dataContext.Users.Where(u => u.RoleIds.Any(r => roleIds.Contains(r))).ToListAsync(cancellationToken);

        var data = roles.Select(r => new GetRolesResponseItem
        {
            Id = r.Id,
            Name = r.Name,
            CreatedBy = r.CreatedBy.Username,
            MemberCount = users.Count(u => u.Roles.Any(ur => ur.RoleId == r.Id)),
            PermissionCount = r.Permissions.Count,
            CreatedAt = r.CreatedAt,
            UpdatedAt = r.UpdatedAt,
            Status = r.Status
        });

        return Result.Ok(new GetRoleListResponse
        {
            Total = total,
            Data = data
        });
    }
}