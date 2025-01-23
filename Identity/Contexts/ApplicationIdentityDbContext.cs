using System.Reflection;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Contexts;

public class ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
    : IdentityDbContext<User>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.HasDefaultSchema("Identity");
    }
}