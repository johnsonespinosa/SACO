using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SACO.Domain.Entities;

namespace SACO.Infrastructure.Data.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("Departments");
        
        builder.HasKey(d => d.Id);
        
        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(d => d.Code)
            .IsRequired()
            .HasMaxLength(50);
            
        // Relationships
        builder.HasOne(d => d.Organ)
            .WithMany(o => o.Departments)
            .HasForeignKey(d => d.OrganId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}