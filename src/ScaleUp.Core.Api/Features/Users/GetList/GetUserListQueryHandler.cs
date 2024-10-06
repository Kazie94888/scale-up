using FluentResults;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Api.Features.Users.Dtos;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Users.GetList;

internal sealed class GetUserListQueryHandler(ReadOnlyMasterDataContext dataContext) : IQueryHandler<GetUserListQuery, GetUserListResponse>
{
    public async Task<Result<GetUserListResponse>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        var userQuery = dataContext.Users;

        var total = await userQuery.CountAsync(cancellationToken);
        var users = await userQuery.OrderByDescending(r => r.CreatedAt).Skip(request.Page * request.PageSize).Take(request.PageSize)
            .ToListAsync(cancellationToken);
        var warehouses = await dataContext.Warehouses.Where(w => users.Any(u => u.WarehouseIds.Contains(w.Id))).ToListAsync(cancellationToken);

        var data = users.Select(u => new GetUserListResponseItem
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email,
            CreatedAt = u.CreatedAt,
            UpdatedAt = u.UpdatedAt,
            Roles = u.Roles.Select(x => new UserRoleDto(x.RoleId, x.RoleName)),
            Warehouses = warehouses.Where(w => u.WarehouseIds.Contains(w.Id)).Select(w => new UserWarehouseDto(w.Id, w.Name)),
            Status = u.Status
        });

        return Result.Ok(new GetUserListResponse
        {
            Total = total,
            Data = data
        });
    }
}