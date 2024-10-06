using ScaleUp.Core.Domain.Entities.AuditLogs;
using ScaleUp.Core.Domain.Entities.Roles;
using ScaleUp.Core.SharedKernel.Entities;

namespace ScaleUp.Core.Domain.Events.Roles.Update;

public sealed class RoleUpdatedEvent : AuditEventBase
{
    public RoleUpdatedEvent(Guid roleId, string name, UserInfo auditedBy) : base(auditedBy)
    {
        Parameters.Add(new AuditLogParameter(nameof(Role.Id), roleId.ToString()));
        Parameters.Add(new AuditLogParameter(nameof(Role.Name), name));

        RoleId = roleId;
        Name = name;
    }
    public Guid RoleId { get; }
    public string Name { get; }
    public override string GetDescription() => $"Role {Name} updated by {AuditedBy.Username}";
}