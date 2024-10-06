using MediatR;
using ScaleUp.Core.Domain.Entities.Tenants;

namespace ScaleUp.Core.Domain.Events.Tenants;

public sealed class TenantCreatedEvent : INotification
{
    public TenantCreatedEvent(Tenant tenant, string adminEmail)
    {
        Tenant = tenant;
        AdminEmail = adminEmail;
    }
    public Tenant Tenant { get; set; }
    public string AdminEmail { get; set; }
}