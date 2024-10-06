using ScaleUp.Core.Domain.Entities.AuditLogs;
using ScaleUp.Core.Domain.Entities.Users;
using ScaleUp.Core.SharedKernel.Entities;

namespace ScaleUp.Core.Domain.Events.Users.Update;

public class UserUpdatedEvent : AuditEventBase
{
    public UserUpdatedEvent(Guid userId, string firstName, string lastName, string email, string? phone, string status, List<Guid> roleIds, List<Guid> warehouseIds, UserInfo auditedBy) : base(auditedBy)
    {
        Parameters.Add(new AuditLogParameter(nameof(User.Id), userId.ToString()));
        Parameters.Add(new AuditLogParameter(nameof(User.FirstName), firstName));
        Parameters.Add(new AuditLogParameter(nameof(User.LastName), lastName));
        Parameters.Add(new AuditLogParameter(nameof(User.Email), email));

        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Status = status;
        RoleIds = roleIds;
        WarehouseIds = warehouseIds;
    }
    public Guid UserId { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string? Phone { get; }
    public string Status { get; }
    public List<Guid> RoleIds { get; }
    public List<Guid> WarehouseIds { get; }
    public override string GetDescription() => $"User {Email} created by {AuditedBy.Username}";
}