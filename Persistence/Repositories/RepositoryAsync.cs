using Application.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories;

public class RepositoryAsync<TEntity>(ApplicationDbContext context) : RepositoryBase<TEntity>(context), IRepositoryAsync<TEntity>
    where TEntity : class{}