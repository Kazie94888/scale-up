using FluentResults;
using MongoDB.Bson.Serialization.Attributes;
using ScaleUp.Core.Domain.Enums;
using ScaleUp.Core.Domain.Events.Tenants;
using ScaleUp.Core.SharedKernel.Entities;
using ScaleUp.Core.SharedKernel.Extensions;

namespace ScaleUp.Core.Domain.Entities.Tenants;

public class Tenant : AggregateRoot
{
    private Tenant()
    {
    }

    [BsonElement("_id")]
    public required Guid Id { get; set; }

    public required string Name { get; set; }
    public required string AdminEmail { get; set; }
    public string? Phone { get; set; }
    public required string Version { get; set; }
    public DateTime? UpdatedAt { get; set; } //Todo: move UpdatedAt to entity
    public DateTime? ActivationEndDate { get; set; }
    public required string ActivationState { get; set; }

    public required HaravanIntegrationConfig HaravanIntegrationConfig { get; set; }

    [BsonIgnore]
    public IReadOnlyList<TenantHistory> History => _history;

    [BsonElement("history")]
    internal List<TenantHistory> _history { get; set; }

    public static Result<Tenant> Create(string name, string adminEmail, string? phone, string version, string activationState,
        DateTime? activationEndDate, UserInfo createBy)
    {
        var tenant = new Tenant
        {
            Id = Guid.NewGuid(),
            Name = name,
            AdminEmail = adminEmail,
            Phone = phone,
            Version = version,
            ActivationState = activationState,
            UpdatedAt = DateTime.UtcNow,
            ActivationEndDate = activationState == TenantActivationState.Inactive.GetDescription() ? DateTime.UtcNow : activationEndDate,
            CreatedBy = createBy,
            HaravanIntegrationConfig = new()
        };

        tenant.AddDomainEvent(new TenantCreatedEvent(tenant, adminEmail));

        return Result.Ok(tenant);
    }

    public void Update(string name, string adminEmail, string? phone, string version, string activationState,
        DateTime? activationEndDate, UserInfo updatedBy)
    {
        AddDomainEvent(new TenantUpdatedEvent(Id, Name, AdminEmail, Phone, Version, ActivationEndDate, ActivationState,
            updatedBy));

        Name = name;
        AdminEmail = adminEmail;
        Phone = phone;
        Version = version;
        ActivationState = activationState;
        UpdatedAt = DateTime.UtcNow;
        ActivationEndDate = activationState == TenantActivationState.Inactive.GetDescription() ? DateTime.UtcNow : activationEndDate;
    }

    public void AddHistory(TenantHistory history)
    {
        _history ??= [];
        _history.Add(history);
    }

}