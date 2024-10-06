using MediatR;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Domain.Entities.Users;
using ScaleUp.Core.Domain.Events.Users.Update;
using ScaleUp.Core.Persistence.Context;

namespace ScaleUp.Core.Application.Users.Update;

internal sealed class UserUpdatedEventHandler(MasterDataContext dataContext) : INotificationHandler<UserUpdatedEvent>
{
    public async Task Handle(UserUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var user = await dataContext.Users.FirstAsync(u => u.Id == notification.UserId, cancellationToken);

        var history = CreateUserHistory(notification.FirstName, notification.LastName, notification.Email, notification.Phone, notification.Status,
            notification.RoleIds, notification.WarehouseIds, notification.AuditedBy.Username);
        user.AddHistory(history);
        
        dataContext.Users.Update(user);
        await dataContext.SaveChangesAsync(cancellationToken);
    }

    private static UserHistory CreateUserHistory(string firstName, string lastName, string email, string? phone, string status, List<Guid> roleIds,
        List<Guid> warehouseIds, string updatedBy)
    {
        return new UserHistory(firstName, lastName, email, phone, status, roleIds, warehouseIds, DateTime.UtcNow, updatedBy);
    }
}