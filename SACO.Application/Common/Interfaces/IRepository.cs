using Ardalis.Specification;

namespace SACO.Application.Common.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class;