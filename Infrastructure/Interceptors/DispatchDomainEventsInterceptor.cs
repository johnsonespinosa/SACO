using Domain.Commons;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Interceptors;

public class DispatchDomainEventsInterceptor(IPublisher publisher) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await DispatchDomainEvents(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task DispatchDomainEvents(DbContext? context)
    {
        if (context == null) return;

        var entities = context.ChangeTracker
            .Entries<Entity>()
            .Where(entry => entry.Entity.DomainEvents.Count != 0)
            .Select(entry => entry.Entity);

        var baseEntities = entities.ToList();
        var domainEvents = baseEntities
            .SelectMany(entity => entity.DomainEvents)
            .ToList();

        baseEntities.ToList().ForEach(entity => entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await publisher.Publish(domainEvent);
    }
}