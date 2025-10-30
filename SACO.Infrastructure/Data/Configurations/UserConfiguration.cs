using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SACO.Domain.Entities;

namespace SACO.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(256);
            
        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(100);
            
        // Relationships
        builder.HasOne(u => u.Department)
            .WithMany(d => d.Users)
            .HasForeignKey(u => u.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.HasOne(u => u.Organ)
            .WithMany(o => o.Users)
            .HasForeignKey(u => u.OrganId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}