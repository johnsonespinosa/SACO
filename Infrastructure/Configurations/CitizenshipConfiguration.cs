using Domain.Circulations.Nomenclatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CitizenshipConfiguration : IEntityTypeConfiguration<Citizenship>
{
    public void Configure(EntityTypeBuilder<Citizenship> builder)
    {
        builder.ToTable(name: "CITIZENSHIPS", schema: "SYSTEM");
        
        builder.HasKey(citizenship => citizenship.CitizenshipId)
            .HasName("PK_CITIZENSHIPS");

        builder.Property(citizenship => citizenship.CitizenshipId)
            .HasColumnName("CITIZENSHIP_ID")
            .HasDefaultValueSql("SYS_GUID()");

        builder.Property(citizenship => citizenship.Abbreviation)
            .HasColumnName("ABBREVIATION")
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(citizenship => citizenship.Description)
            .HasColumnName("DESCRIPTION")
            .HasMaxLength(100)
            .IsRequired();
    }
}