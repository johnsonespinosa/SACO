using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SACO.Domain.Common;
using SACO.Domain.Entities;

namespace SACO.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
{
    public DbSet<Passenger> Passengers => Set<Passenger>();
    public DbSet<Circulation> Circulations => Set<Circulation>();
    public DbSet<Organ> Organs => Set<Organ>();
    public DbSet<Department> Departments => Set<Department>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Ignore BaseEvent because it's not an entity
        modelBuilder.Ignore<BaseEvent>();
        
        // Apply all settings from this assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}