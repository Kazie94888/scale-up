using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.SharedKernel.Entities;

namespace ScaleUp.Core.Persistence.DomainEvents;

public sealed class DomainEventsDispatcher : IDomainEventsDispatcher
{
    private readonly IMediator _mediator;
    private readonly ILogger<DomainEventsDispatcher> _logger;

    public DomainEventsDispatcher(IMediator mediator, ILogger<DomainEventsDispatcher> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task DispatchDomainEventsAsync<TContext>(TContext context, CancellationToken cancellationToken) where TContext : DbContext
    {
        var entities = context.ChangeTracker.Entries<AggregateRoot>()
            .Where(x => x.Entity.DomainEvents.Any())
            .Select(x => x.Entity)
            .ToList();
        var events = entities.SelectMany(x => x.DomainEvents).ToList();
        entities.ForEach(x => x.ClearDomainEvents());

        foreach (var @event in events)
        {
            var domainEventName = @event.GetType().Name;
            var eventData = JsonSerializer.Serialize(@event);
            try
            {
                await _mediator.Publish(@event, cancellationToken);
                _logger.LogDebug("Published domain event {EventName}. EventData - {EventData}", domainEventName, eventData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to publish domain event {EventName}. EventData - {EventData}", domainEventName, eventData);
                throw;
            }
        }
    }
}
