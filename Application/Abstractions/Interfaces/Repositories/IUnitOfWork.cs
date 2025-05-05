using System.Data;

namespace Application.Abstractions.Interfaces.Repositories;

public interface IUnitOfWork
{
    Task<T> ExecuteTransactionalAsync<T>(
        Func<CancellationToken, Task<T>> operation,
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken cancellationToken = default);
    
    Task<IUnitOfWorkTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken cancellationToken = default);
    
    Task CommitAsync(CancellationToken cancellationToken = default);
}