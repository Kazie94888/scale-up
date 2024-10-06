using Microsoft.EntityFrameworkCore;

namespace ScaleUp.Core.Persistence.DomainEvents;

public interface IDomainEventsDispatcher
{
    Task DispatchDomainEventsAsync<TContext>(TContext context, CancellationToken cancellationToken) where TContext : DbContext;
}