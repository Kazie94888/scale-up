using FluentResults;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Domain.Entities.Users;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Constants;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Roles.Update;

internal sealed class UpdateRoleCommandHandler(MasterDataContext dataContext) : ICommandHandler<UpdateRoleCommand, UpdateRoleResponse>
{
    public async Task<Result<UpdateRoleResponse>> Handle(UpdateRoleCommand command, CancellationToken cancellationToken)
    {
        var role = await dataContext.Roles.FirstAsync(r => r.Id == command.RoleId, cancellationToken);

        if (!role.Name.Equals(command.Request.Name))
        {
            var roleExisted = await dataContext.Roles.AnyAsync(r =>
                r.Name.Equals(command.Request.Name), cancellationToken);
            if (roleExisted)
                return Result.Fail(string.Format(ErrorConstants.RoleExisted, command.Request.Name));
        }
        
        var usersToRemoveRole = await dataContext.Users
            .Where(u => u.RoleIds.Any(id => id == role.Id)).Include(user => user.Roles)
            .ToListAsync(cancellationToken);
        foreach (var user in usersToRemoveRole)
        {
            var roleToRemove = user.Roles.First(x => x.RoleId == role.Id);
            
            user.RemoveRole(roleToRemove);
        };
        
        dataContext.Users.UpdateRange(usersToRemoveRole);

        role.Update(command.Request.Name, command.Request.Permissions, command.Request.Status, command.UserInfo);

        var updatedRole = new UserRole(role.Id, role.Name);
        var usersToUpdate = await dataContext.Users.Where(u => command.Request.UserIds.Contains(u.Id)).ToListAsync(cancellationToken);
        foreach (var user in usersToUpdate) user.AddRole(updatedRole);
        
        dataContext.Users.UpdateRange(usersToUpdate);

        dataContext.Roles.Update(role);
        
        await dataContext.SaveChangesAsync(cancellationToken);

        return Result.Ok(new UpdateRoleResponse
        {
            RoleId = command.RoleId
        });
    }
}