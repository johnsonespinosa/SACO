using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SACO.Domain.Entities;

namespace SACO.Infrastructure.Data.Configurations;

public class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
{
    public void Configure(EntityTypeBuilder<Passenger> builder)
    {
        builder.ToTable("Passengers");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(p => p.SecondName)
            .HasMaxLength(100);
            
        builder.Property(p => p.FirstLastName)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(p => p.SecondLastName)
            .HasMaxLength(100);
            
        builder.Property(p => p.BirthDate)
            .IsRequired();
            
        builder.Property(p => p.Citizenship)
            .IsRequired()
            .HasMaxLength(100);
            
        // Configure SearchKey as a persisted property
        builder.Property(p => p.SearchKey)
            .IsRequired()
            .HasMaxLength(255);
            
        // Ignore calculated properties that should not be persisted
        builder.Ignore(p => p.FullName);
        
        // Index for efficient searches
        builder.HasIndex(p => p.SearchKey);
        builder.HasIndex(p => new { p.FirstLastName, p.FirstName, p.BirthDate });
    }
}