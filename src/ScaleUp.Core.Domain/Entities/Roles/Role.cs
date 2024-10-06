using MongoDB.Bson.Serialization.Attributes;
using ScaleUp.Core.Domain.Base.Interfaces;
using ScaleUp.Core.Domain.Events.Roles;
using ScaleUp.Core.Domain.Events.Roles.Create;
using ScaleUp.Core.Domain.Events.Roles.Update;
using ScaleUp.Core.SharedKernel.Entities;
using ScaleUp.Core.SharedKernel.Enums;
using ScaleUp.Core.SharedKernel.Extensions;

namespace ScaleUp.Core.Domain.Entities.Roles;

public sealed class Role : AggregateRoot, IMultiTenant
{
    private Role()
    {
    }

    [BsonElement("_id")]
    public required Guid Id { get; set; }

    public required string Name { get; set; }
    public required List<string> Permissions { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public required string Status { get; set; }
    public required string Type { get; set; }
    public Guid TenantId { get; set; }

    public static Role Create(string name, List<string> permissions, string status, UserInfo createdBy, RoleType type)
    {
        var role = new Role
        {
            CreatedAt = DateTime.UtcNow,
            CreatedBy = createdBy,
            Id = Guid.NewGuid(),
            Name = name,
            Permissions = permissions,
            UpdatedAt = null,
            Status = status,
            Type = type.GetDescription()
        };

        role.AddDomainEvent(new RoleCreatedEvent(role, createdBy));
        return role;
    }

    public void Update(string name, List<string> permissions, string status, UserInfo updatedBy)
    {
        Name = name;
        Permissions = permissions;
        Status = status;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new RoleUpdatedEvent(Id, Name, updatedBy));
    }
}