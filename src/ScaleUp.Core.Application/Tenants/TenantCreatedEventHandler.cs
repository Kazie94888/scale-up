using MediatR;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Domain.Entities.Roles;
using ScaleUp.Core.Domain.Entities.Users;
using ScaleUp.Core.Domain.Events.Tenants;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Constants;
using ScaleUp.Core.SharedKernel.Entities;
using ScaleUp.Core.SharedKernel.Enums;
using ScaleUp.Core.SharedKernel.Extensions;

namespace ScaleUp.Core.Application.Tenants;

public class TenantCreatedEventHandler(MasterDataContext dataContext) : INotificationHandler<TenantCreatedEvent>
{
    public async Task Handle(TenantCreatedEvent notification, CancellationToken cancellationToken)
    {
        var role = Role.Create(RoleConstants.TenantAdmin, [], RoleStatus.Active.GetDescription(), notification.Tenant.CreatedBy, RoleType.System);
        dataContext.Roles.Add(role);

        var userRole = new UserRole(role.Id, role.Name);

        var user = await dataContext.Users.FirstOrDefaultAsync(
            o => o.Email.Equals(notification.AdminEmail, StringComparison.OrdinalIgnoreCase), cancellationToken);
        if (user is null)
        {
            var userResult = User.Create(
                string.Empty,
                string.Empty,
                notification.AdminEmail,
                null,
                UserStatus.Active.GetDescription(),
                [],
                UserInfo.System
            );

            if (userResult.IsFailed)
                return;

            userResult.Value.AddRole(userRole);
            dataContext.Users.Add(userResult.Value);
        }
        else
        {
            user.AddRole(userRole);
            dataContext.Users.Update(user);
        }

        await dataContext.SaveChangesAsync(cancellationToken);
    }
}