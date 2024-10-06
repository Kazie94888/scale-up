using MediatR;

namespace ScaleUp.Core.SharedKernel.Entities;

public abstract class AggregateRoot : Entity
{
    private readonly List<INotification> _domainEvents = [];

    public IReadOnlyList<INotification> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(INotification eventItem)
    {
        _domainEvents.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
