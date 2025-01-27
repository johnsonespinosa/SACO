using Domain.Entities;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<CirculationType> Circulations { get; }
    DbSet<Citizenship> Citizenships { get; }
    DbSet<Expiration> Expirations { get; }
    DbSet<Circulation> OperationalCirculations { get; }
    DbSet<Organ> Organs { get; }
}