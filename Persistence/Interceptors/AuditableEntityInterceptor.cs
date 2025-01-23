using Application.Interfaces;
using Domain.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Interceptors;

public class AuditableEntityInterceptor(
    IUser user,
    ITimeProvider dateTime) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
        {
            var utcNow = dateTime.UtcNow;
            switch (entry.State)
            {
                case EntityState.Added:
                    if (user?.Id != null)
                        entry.Entity.CreatedBy = user.Id;
                    entry.Entity.Created = utcNow;
                    break;
                case EntityState.Modified:
                    if (user?.Id != null)
                        entry.Entity.LastModifiedBy = user.Id;
                    entry.Entity.LastModified = utcNow;
                    break;
                case EntityState.Deleted:
                    entry.Entity.Deleted = utcNow;
                    if (user?.Id != null)
                        entry.Entity.DeletedBy = user.Id;
                    entry.State = EntityState.Modified;
                    break;
            }
        }
    }
}