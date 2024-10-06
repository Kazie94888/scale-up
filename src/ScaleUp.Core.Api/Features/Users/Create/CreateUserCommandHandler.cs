using FluentResults;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Domain.Entities.Users;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Constants;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Users.Create;

internal sealed class CreateUserCommandHandler(MasterDataContext dataContext) : ICommandHandler<CreateUserCommand, CreateUserResponse>
{
    public async Task<Result<CreateUserResponse>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var userExisted = await dataContext.Users.AnyAsync(u => u.Email.Equals(command.User.Email, StringComparison.OrdinalIgnoreCase), cancellationToken);
        if (userExisted) return Result.Fail(string.Format(ErrorConstants.EmailExisted, nameof(User), command.User.Email));

        var result = User.Create(command.User.FirstName, command.User.LastName, command.User.Email, command.User.Phone, command.User.Status,
            command.User.WarehouseIds, command.UserInfo);

        if (result.IsFailed)
            return Result.Fail(result.Errors);

        var roles = await dataContext.Roles.Where(r => command.User.RoleIds.Contains(r.Id)).ToListAsync(cancellationToken);
        foreach (var role in roles)
        {
            var userRole = new UserRole(role.Id, role.Name);
            result.Value.AddRole(userRole);
        }

        dataContext.Users.Add(result.Value);
        await dataContext.SaveChangesAsync(cancellationToken);

        return Result.Ok(new CreateUserResponse
        {
            EntityId = result.Value.Id
        });
    }
}