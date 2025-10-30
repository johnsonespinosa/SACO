using SACO.Application.Common.Interfaces;
using SACO.Domain.Entities;
using SACO.Infrastructure.Data;

namespace SACO.Infrastructure.Repositories;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private readonly Dictionary<Type, object> _repositories = new();

    public IRepository<User> Users => GetRepository<User>();
    public IRepository<Passenger> Passengers => GetRepository<Passenger>();
    public IRepository<Circulation> Circulations => GetRepository<Circulation>();
    public IRepository<Organ> Organs => GetRepository<Organ>();
    public IRepository<Department> Departments => GetRepository<Department>();

    private IRepository<T> GetRepository<T>() where T : class
    {
        var type = typeof(T);
        
        if (_repositories.TryGetValue(type, out var value)) return (IRepository<T>)value;
        
        value = new Repository<T>(context);
        _repositories[type] = value;
        
        return (IRepository<T>)value;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public void Dispose()
    {
        context.Dispose();
    }
}