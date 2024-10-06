using MongoDB.Bson.Serialization.Attributes;

namespace ScaleUp.Core.Domain.Entities.Orders;

public sealed record OrderAssignee(Guid Id, string FirstName, string? LastName, string? Email)
{

    [BsonIgnore]
    public string FullName => $"{FirstName ?? ""} {LastName ?? ""}";
}