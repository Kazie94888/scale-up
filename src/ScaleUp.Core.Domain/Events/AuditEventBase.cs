using MediatR;
using ScaleUp.Core.Domain.Entities.AuditLogs;
using ScaleUp.Core.SharedKernel.Entities;

namespace ScaleUp.Core.Domain.Events;

public abstract class AuditEventBase : INotification
{
    protected AuditEventBase(UserInfo auditedBy)
    {
        Parameters = [];
        AuditedBy = auditedBy;
        Timestamp = DateTimeOffset.UtcNow;
    }

    public DateTimeOffset Timestamp { get; }
    public UserInfo AuditedBy { get; }

    public List<AuditLogParameter> Parameters { get; }

    public abstract string GetDescription();
}