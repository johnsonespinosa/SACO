using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SACO.Domain.Entities;

namespace SACO.Infrastructure.Data.Configurations;

public class CirculationConfiguration : IEntityTypeConfiguration<Circulation>
{
    public void Configure(EntityTypeBuilder<Circulation> builder)
    {
        builder.ToTable("Circulations");
        
        builder.HasKey(c => c.Id);
        
        // Set ExpeditionNumber to normal string
        builder.Property(c => c.ExpeditionNumber)
            .IsRequired()
            .HasMaxLength(20);
            
        builder.Property(c => c.OfficerPhone)
            .HasMaxLength(20);
            
        // Configure TraceKey as a persisted property
        builder.Property(c => c.TraceKey)
            .IsRequired()
            .HasMaxLength(255);
        
        // Indexes for efficient searches
        builder.HasIndex(c => c.ExpeditionNumber).IsUnique();
        builder.HasIndex(c => c.Status);
        builder.HasIndex(c => c.Type);
        builder.HasIndex(c => c.ExpirationDate);
        builder.HasIndex(c => c.TraceKey);
        
        // Ignore calculated properties
        builder.Ignore(c => c.IsExpired);
        builder.Ignore(c => c.CanBeValidated);
        builder.Ignore(c => c.IsEffective);
        builder.Ignore(c => c.IsOperative);
        builder.Ignore(c => c.ExpeditionPrefix);
        builder.Ignore(c => c.ExpeditionSequence);
        
        // Relationships
        builder.HasOne(c => c.Passenger)
            .WithMany(p => p.Circulations)
            .HasForeignKey(c => c.PassengerId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.HasOne(c => c.Organ)
            .WithMany(o => o.Circulations)
            .HasForeignKey(c => c.OrganId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.HasOne(c => c.Department)
            .WithMany(d => d.Circulations)
            .HasForeignKey(c => c.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}