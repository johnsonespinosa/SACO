using Application.Abstractions.Interfaces.Services;
using Domain.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Interceptors;

public class AuditableEntityInterceptor(
    ICurrentUserService currentUser,
    IDateTimeService dateTime) : SaveChangesInterceptor
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

        foreach (var entry in context.ChangeTracker.Entries<AuditEntity>())
        {
            var utcNow = dateTime.UtcNow;
            switch (entry.State)
            {
                case EntityState.Added:
                    if (currentUser?.UserId != null)
                        entry.Entity.CreatedBy = currentUser.UserId;
                    entry.Entity.CreatedAt = utcNow;
                    break;
                case EntityState.Modified:
                    if (currentUser?.UserId != null)
                        entry.Entity.UpdatedBy = currentUser.UserId;
                    entry.Entity.UpdatedAt = utcNow;
                    break;
                case EntityState.Deleted:
                    entry.Entity.UpdatedAt = utcNow;
                    entry.Entity.IsDeleted = true;
                    if (currentUser?.UserId != null)
                        entry.Entity.UpdatedBy = currentUser.UserId;
                    entry.State = EntityState.Modified;
                    break;
            }
        }
    }
}