using FluentResults;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Domain.Entities.Roles;
using ScaleUp.Core.Domain.Entities.Users;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Constants;
using ScaleUp.Core.SharedKernel.Enums;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Roles.Create;

internal sealed class CreateRoleCommandHandler(MasterDataContext dataContext)
    : ICommandHandler<CreateRoleCommand, CreateRoleResponse>
{
    public async Task<Result<CreateRoleResponse>> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
    {
        var roleExisted = await dataContext.Roles.AnyAsync(r =>
            r.Name.Equals(command.Request.Name), cancellationToken);
        if (roleExisted)
            return Result.Fail(string.Format(ErrorConstants.RoleExisted, command.Request.Name));

        var role = Role.Create(command.Request.Name, command.Request.PermissionIds, command.Request.Status,
            command.UserInfo, RoleType.Tenant);
        dataContext.Roles.Add(role);

        var users = await dataContext.Users.Where(u => command.Request.UserIds.Contains(u.Id)).ToListAsync(cancellationToken);
        var userRole = new UserRole(role.Id, role.Name);
        foreach (var user in users) user.AddRole(userRole);
        dataContext.Users.UpdateRange(users);

        await dataContext.SaveChangesAsync(cancellationToken);
        
        return Result.Ok(new CreateRoleResponse
        {
            RoleId = role.Id
        });
    }
}