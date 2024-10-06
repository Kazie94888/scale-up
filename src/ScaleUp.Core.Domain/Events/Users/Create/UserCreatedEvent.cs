using ScaleUp.Core.Domain.Entities.AuditLogs;
using ScaleUp.Core.Domain.Entities.Users;
using ScaleUp.Core.SharedKernel.Entities;

namespace ScaleUp.Core.Domain.Events.Users.Create;

public sealed class UserCreatedEvent : AuditEventBase
{
    public UserCreatedEvent(Guid userId, string firstName, string lastName, string email, UserInfo auditedBy) : base(auditedBy)
    {
        Parameters.Add(new AuditLogParameter(nameof(User.Id), userId.ToString()));
        Parameters.Add(new AuditLogParameter(nameof(User.FirstName), firstName));
        Parameters.Add(new AuditLogParameter(nameof(User.LastName), lastName));
        Parameters.Add(new AuditLogParameter(nameof(User.Email), email));

        Email = email;
    }

    public string Email { get; }

    public override string GetDescription() => $"User {Email} created by {AuditedBy.Username}";
}