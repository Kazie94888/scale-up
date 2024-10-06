using FluentResults;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Domain.Entities.Users;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Roles.Get;

internal sealed class GetRoleQueryHandler(ReadOnlyMasterDataContext dataContext) : IQueryHandler<GetRoleQuery, GetRoleQueryResponse>
{
    public async Task<Result<GetRoleQueryResponse>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        var role = await dataContext.Roles.FirstAsync(r => r.Id == request.RoleId, cancellationToken);
        var users = await dataContext.Users
            .Where(u => u.RoleIds.Any(id => id == role.Id)).Include(user => user.Roles)
            .ToListAsync(cancellationToken);
        return Result.Ok(new GetRoleQueryResponse
        {
            Id = role.Id,
            Name = role.Name,
            PermissionIds = role.Permissions,
            Status = role.Status,
            Type = role.Type,
            CreatedBy = role.CreatedBy.Username,
            CreatedAt = role.CreatedAt,
            UpdatedAt = role.UpdatedAt,
            Users = users.Select(user => new RoleUserDto { Id = user.Id, Name = $"{user.FirstName} {user.LastName}", Email = user.Email }).ToList()
        });
    }
}