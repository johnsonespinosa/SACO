using Domain.Circulations.Nomenclatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class OrganConfiguration : IEntityTypeConfiguration<Organ>
{
    public void Configure(EntityTypeBuilder<Organ> builder)
    {
        builder.ToTable(name: "ORGANS", schema: "SYSTEM");
        
        builder.HasKey(organ => organ.OrganId)
            .HasName("PK_ORGANS");

        builder.Property(organ => organ.OrganId)
            .HasColumnName("ORGAN_ID")
            .HasDefaultValueSql("SYS_GUID()");

        builder.Property(organ => organ.Name)
            .HasColumnName("NAME")
            .HasMaxLength(100)
            .IsRequired();
    }
}