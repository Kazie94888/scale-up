using ScaleUp.Core.Domain.Entities.AuditLogs;
using ScaleUp.Core.Domain.Entities.Tenants;
using ScaleUp.Core.SharedKernel.Entities;

namespace ScaleUp.Core.Domain.Events.Tenants;

public class TenantUpdatedEvent : AuditEventBase
{
    public TenantUpdatedEvent(Guid tenantId, string name, string adminEmail, string? phone, string version,
        DateTime? activationEndDate, string activationState, UserInfo userInfo) : base(userInfo)
    {
        Parameters.Add(new AuditLogParameter(nameof(Tenant.Id), tenantId.ToString()));
        Parameters.Add(new AuditLogParameter(nameof(Tenant.Name), name));

        TenantId = tenantId;
        Name = name;
        AdminEmail = adminEmail;
        Phone = phone;
        Version = version;
        ActivationEndDate = activationEndDate;
        ActivationState = activationState;
        UpdatedAt = DateTime.UtcNow;
    }

    public Guid TenantId { get; }
    public string Name { get; }
    public string AdminEmail { get; set; }
    public string? Phone { get; set; }
    public string Version { get; set; }
    public DateTime? ActivationEndDate { get; set; }
    public string ActivationState { get; set; }
    public DateTime UpdatedAt { get; }

    public override string GetDescription()
    {
        return $"Tenant {Name} updated by {AuditedBy.Username}";
    }
}