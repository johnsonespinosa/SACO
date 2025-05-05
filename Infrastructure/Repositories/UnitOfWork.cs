using System.Data;
using Application.Abstractions.Interfaces.Repositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public async Task<T> ExecuteTransactionalAsync<T>(
        Func<CancellationToken, Task<T>> operation,
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken cancellationToken = default)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(isolationLevel, cancellationToken);
        try
        {
            var result = await operation(cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return result;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<IUnitOfWorkTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken cancellationToken = default)
    {
        var transaction = await context.Database.BeginTransactionAsync(
            isolationLevel, 
            cancellationToken);
        
        return new UnitOfWorkTransaction(transaction);
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}