using Domain.Entities;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Circulation> Circulations { get; }
    DbSet<Citizenship> Citizenships { get; }
    DbSet<Expiration> Expirations { get; }
    DbSet<OperationalCirculation> OperationalCirculations { get; }
    DbSet<Organ> Organs { get; }
}