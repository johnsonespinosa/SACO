using Domain.Circulations.Nomenclatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CirculationTypeConfiguration : IEntityTypeConfiguration<CirculationType>
{
    public void Configure(EntityTypeBuilder<CirculationType> builder)
    {
        builder.ToTable(name: "CIRCULATION_TYPES", schema: "SYSTEM");
        
        builder.HasKey(circulationType => circulationType.CirculationTypeId)
            .HasName("PK_CIRCULATION_TYPES");

        builder.Property(circulationType => circulationType.CirculationTypeId)
            .HasColumnName("CIRCULATION_TYPE_ID")
            .HasDefaultValueSql("SYS_GUID()");

        builder.Property(circulationType => circulationType.Abbreviation)
            .HasColumnName("ABBREVIATION")
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(circulationType => circulationType.Description)
            .HasColumnName("DESCRIPTION")
            .HasMaxLength(100)
            .IsRequired();
    }
}