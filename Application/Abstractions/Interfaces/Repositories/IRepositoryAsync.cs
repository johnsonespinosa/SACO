using Ardalis.Specification;

namespace Application.Abstractions.Interfaces.Repositories;

public interface IRepositoryAsync<TEntity> : IRepositoryBase<TEntity> 
    where TEntity : class;