using FluentResults;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Domain.Entities.Users;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Constants;
using ScaleUp.Core.SharedKernel.Extensions;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Users.Update;

internal sealed class UpdateUserCommandHandler(MasterDataContext dataContext) : ICommandHandler<UpdateUserCommand, UpdateUserResponse>
{
    public async Task<Result<UpdateUserResponse>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await dataContext.Users.Include(user => user.Roles).FirstAsync(u => u.Id == command.UserId, cancellationToken);

        if (!user.Email.EqualsTo(command.Request.Email))
        {
            var userExisted =
                await dataContext.Users.AnyAsync(u => u.Email.Equals(command.Request.Email, StringComparison.OrdinalIgnoreCase), cancellationToken);
            if (userExisted)
                return Result.Fail(string.Format(ErrorConstants.EmailExisted, nameof(User), command.Request.Email));
        }

        user.Update(command.Request.FirstName, command.Request.LastName, command.Request.Email, command.Request.Phone, command.Request.Status,
            command.Request.WarehouseIds, command.UserInfo);

        var rolesToRemove = await dataContext.Roles.Where(r => user.RoleIds.Contains(r.Id)).ToListAsync(cancellationToken);
        foreach (var role in rolesToRemove)
        {
            var userRole = user.Roles.First(r => r.RoleId == role.Id);
            user.RemoveRole(userRole);
        }

        var rolesToUpdate = await dataContext.Roles.Where(r => command.Request.RoleIds.Contains(r.Id)).ToListAsync(cancellationToken);
        foreach (var role in rolesToUpdate)
        {
            var userRole = new UserRole(role.Id, role.Name);
            user.AddRole(userRole);
        }

        dataContext.Users.Update(user);
        await dataContext.SaveChangesAsync(cancellationToken);

        return Result.Ok(new UpdateUserResponse
        {
            UserId = command.UserId
        });
    }
}