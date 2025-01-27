using System.Reflection;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly AuditableEntityInterceptor _auditableEntityInterceptor = null!;
    private readonly DispatchDomainEventsInterceptor _domainEventsInterceptor = null!;
    
    // Constructor para inyección de dependencias
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        AuditableEntityInterceptor auditableEntityInterceptor,
        DispatchDomainEventsInterceptor domainEventsInterceptor) : base(options)
    {
        _auditableEntityInterceptor = auditableEntityInterceptor ??
                                      throw new ArgumentNullException(nameof(auditableEntityInterceptor));
        _domainEventsInterceptor = domainEventsInterceptor ??
                                   throw new ArgumentNullException(nameof(domainEventsInterceptor));
    }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
    
    public DbSet<CirculationType> Circulations => Set<CirculationType>();
    public DbSet<Citizenship> Citizenships => Set<Citizenship>();
    public DbSet<Expiration> Expirations => Set<Expiration>();
    public DbSet<Circulation> OperationalCirculations => Set<Circulation>();
    public DbSet<Organ> Organs => Set<Organ>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntityInterceptor);
    }

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