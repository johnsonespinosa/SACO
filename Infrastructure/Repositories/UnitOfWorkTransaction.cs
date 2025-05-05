using Application.Abstractions.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repositories;

internal sealed class UnitOfWorkTransaction(IDbContextTransaction transaction) : IUnitOfWorkTransaction
{
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await transaction.CommitAsync(cancellationToken);
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        await transaction.RollbackAsync(cancellationToken);
    }

    public void Dispose()
    {
        transaction.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await transaction.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}