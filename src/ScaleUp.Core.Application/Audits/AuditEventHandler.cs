using MediatR;
using ScaleUp.Core.Domain.Entities.AuditLogs;
using ScaleUp.Core.Domain.Events;
using ScaleUp.Core.Persistence.Context;

namespace ScaleUp.Core.Application.Audits;

internal class AuditEventHandler(MasterDataContext dataContext) : INotificationHandler<AuditEventBase>
{
    public async Task Handle(AuditEventBase notification, CancellationToken cancellationToken)
    {
        var parameters = notification.Parameters.ToList();

        var auditLog = AuditLog.Create(
                  notification.GetType().Name,
                  notification.GetDescription(),
                  parameters,
                  DateTimeOffset.UtcNow);

        dataContext.AuditLogs.Add(auditLog);

        await dataContext.SaveChangesAsync(cancellationToken);
    }
}
