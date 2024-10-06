using FluentResults;
using MongoDB.Bson.Serialization.Attributes;
using ScaleUp.Core.Domain.Base.Interfaces;
using ScaleUp.Core.Domain.Events.Users;
using ScaleUp.Core.Domain.Events.Users.Create;
using ScaleUp.Core.Domain.Events.Users.Update;
using ScaleUp.Core.SharedKernel.Entities;
using ScaleUp.Core.SharedKernel.Enums;
using ScaleUp.Core.SharedKernel.Extensions;

namespace ScaleUp.Core.Domain.Entities.Users;

public class User : AggregateRoot, IMultiTenant
{
    private User()
    {
    }

    public static Result<User> Create(string firstName, string lastName, string email, string? phone, string status, List<Guid> warehouseIds, UserInfo createdBy)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Phone = phone,
            CreatedBy = createdBy,
            Roles = [],
            RoleIds = [],
            WarehouseIds = warehouseIds,
            Status = status
        };

        user.AddDomainEvent(new UserCreatedEvent(user.Id, firstName, lastName, email, createdBy));

        return Result.Ok(user);
    }

    [BsonElement("_id")]
    public required Guid Id { get; set; }

    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    [BsonIgnore]
    public string FullName => $"{FirstName} {LastName}";
    
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public List<UserRole> Roles { get; set; } = [];
    public List<Guid> RoleIds { get; set; }
    public List<Guid> WarehouseIds { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public required string Status { get; set; }
    [BsonIgnore]
    public IReadOnlyList<UserHistory> History => _history;

    [BsonElement("history")]
    internal List<UserHistory> _history { get; set; }

    public Guid TenantId { get; set; }

    public void AddRole(UserRole userRole)
    {
        RoleIds.Add(userRole.RoleId);
        Roles.Add(userRole);
    }

    public void RemoveRole(UserRole userRole)
    {
        RoleIds.Remove(userRole.RoleId);
        Roles.Remove(userRole);
    }

    public void Update(string firstName, string lastName, string email, string? phone, string status, List<Guid> warehouseIds, UserInfo updatedBy)
    {
        AddDomainEvent(new UserUpdatedEvent(Id, FirstName, LastName, Email, Phone, Status, RoleIds, WarehouseIds, updatedBy));
        
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Status = status;
        WarehouseIds = warehouseIds;
    }
    
    public void AddHistory(UserHistory history)
    {
        _history ??= [];
        _history.Add(history);
    }
}