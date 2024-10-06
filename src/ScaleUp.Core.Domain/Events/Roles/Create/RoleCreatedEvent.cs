using ScaleUp.Core.Domain.Entities.AuditLogs;
using ScaleUp.Core.Domain.Entities.Roles;
using ScaleUp.Core.SharedKernel.Entities;

namespace ScaleUp.Core.Domain.Events.Roles.Create;

public sealed class RoleCreatedEvent : AuditEventBase
{
    public RoleCreatedEvent(Role role, UserInfo auditedBy) : base(auditedBy)
    {
        Parameters.Add(new AuditLogParameter(nameof(Entities.Roles.Role.Id), role.Id.ToString()));
        Parameters.Add(new AuditLogParameter(nameof(Entities.Roles.Role.Name), role.Name));

        Role = role;
    }

    public Role Role { get; }
    public override string GetDescription() => $"Role {Role.Name} created by {AuditedBy.Username}";
}