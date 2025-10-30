using Ardalis.Specification.EntityFrameworkCore;
using SACO.Application.Common.Interfaces;
using SACO.Infrastructure.Data;

namespace SACO.Infrastructure.Repositories;

public class Repository<T>(ApplicationDbContext dbContext) : RepositoryBase<T>(dbContext), IRepository<T>
    where T : class;