using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SACO.Domain.Entities;

namespace SACO.Infrastructure.Data.Configurations;

public class OrganConfiguration : IEntityTypeConfiguration<Organ>
{
    public void Configure(EntityTypeBuilder<Organ> builder)
    {
        builder.ToTable("Organs");
        
        builder.HasKey(o => o.Id);
        
        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(o => o.Code)
            .IsRequired()
            .HasMaxLength(50);
    }
}