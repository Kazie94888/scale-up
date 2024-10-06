using MongoDB.Bson;

namespace ScaleUp.Core.Api.Base.Dtos;

public sealed record EntityId
{
    public EntityId(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}