using System.Reflection;
using Domain.Authentication;
using Domain.Circulations;
using Domain.Circulations.Nomenclatures;
using Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): IdentityDbContext<User>(options)
{
    public DbSet<Circulation> Circulations => Set<Circulation>();
    public DbSet<CirculationType> CirculationTypes => Set<CirculationType>();
    public DbSet<Citizenship> Citizenships => Set<Citizenship>();
    public DbSet<Expiration> Expirations => Set<Expiration>();
    public DbSet<Organ> Organs => Set<Organ>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("SYSTEM");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}