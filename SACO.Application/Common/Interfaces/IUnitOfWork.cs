using SACO.Domain.Entities;

namespace SACO.Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get; }
    IRepository<Passenger> Passengers { get; }
    IRepository<Circulation> Circulations { get; }
    IRepository<Organ> Organs { get; }
    IRepository<Department> Departments { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
}