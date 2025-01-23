using System.Reflection;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Circulation> Circulations => Set<Circulation>();
    public DbSet<Citizenship> Citizenships => Set<Citizenship>();
    public DbSet<Expiration> Expirations => Set<Expiration>();
    public DbSet<OperationalCirculation> OperationalCirculations => Set<OperationalCirculation>();
    public DbSet<Organ> Organs => Set<Organ>();

    public ApplicationDbContext(){}

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    
        modelBuilder.HasDefaultSchema("System");

        modelBuilder.HasSequence(name: "IDGEN");

        modelBuilder.HasSequence(name: "LOG_SEQUENCE");

        modelBuilder.HasSequence(name: "Login_SEQ");

        modelBuilder.HasSequence(name: "SEQ_CIRCULADOLC_ID").IsCyclic();

        modelBuilder.HasSequence(name: "SEQ_CUB_INADM").IsCyclic();

        modelBuilder.HasSequence(name: "SEQ_DOC_INAC").IsCyclic();

        modelBuilder.HasSequence(name: "SQ_Login");
    }
}