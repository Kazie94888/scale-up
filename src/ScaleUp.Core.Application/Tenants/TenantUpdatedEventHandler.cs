using MediatR;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Domain.Entities.Tenants;
using ScaleUp.Core.Domain.Entities.Users;
using ScaleUp.Core.Domain.Events.Tenants;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Constants;
using ScaleUp.Core.SharedKernel.Entities;
using ScaleUp.Core.SharedKernel.Enums;
using ScaleUp.Core.SharedKernel.Extensions;

namespace ScaleUp.Core.Application.Tenants;

internal sealed class TenantUpdatedEventHandler(MasterDataContext dataContext)
    : INotificationHandler<TenantUpdatedEvent>
{
    public async Task Handle(TenantUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var tenant = await dataContext.Tenants.FirstAsync(x => x.Id == notification.TenantId, cancellationToken);
        var role = await dataContext.Roles.FirstAsync(
            r => r.Name.Equals(RoleConstants.TenantAdmin), cancellationToken);

        var adminTenantUser = await dataContext.Users.FirstAsync(x => x.RoleIds.Contains(role.Id), cancellationToken);
        if (!notification.AdminEmail.EqualsTo(adminTenantUser.Email))
        {
            adminTenantUser.RemoveRole(new UserRole(role.Id, RoleConstants.TenantAdmin));
            dataContext.Users.Update(adminTenantUser);

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

                userResult.Value.AddRole(new UserRole(role.Id, role.Name));
                dataContext.Users.Add(userResult.Value);
            }
            else
            {
                user.AddRole(new UserRole(role.Id, role.Name));
                dataContext.Users.Update(user);
            }
        }

        var history = CreateTenantHistory(notification.Name, notification.AdminEmail, notification.Phone,
            notification.Version, notification.ActivationEndDate, notification.ActivationState, notification.UpdatedAt);
        tenant.AddHistory(history);

        dataContext.Tenants.Update(tenant);
        await dataContext.SaveChangesAsync(cancellationToken);
    }

    private static TenantHistory CreateTenantHistory(string name, string adminEmail, string? phone, string version,
        DateTime? activationEndDate, string activationState, DateTime updatedAt)
    {
        return new TenantHistory(name,
            adminEmail, phone, version, activationEndDate, activationState, updatedAt);
    }
}