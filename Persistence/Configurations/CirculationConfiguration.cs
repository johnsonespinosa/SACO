using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CirculationConfiguration : IEntityTypeConfiguration<Circulation>
{
    public void Configure(EntityTypeBuilder<Circulation> builder)
    {
        builder.HasKey(circulationCode => circulationCode.Id);

        builder.ToTable("Circulations");

        builder.Property(circulationCode => circulationCode.Abbreviation)
            .HasMaxLength(4)
            .IsUnicode(false);

        builder.Property(circulationCode => circulationCode.Description)
            .HasMaxLength(25)
            .IsUnicode(false);

        builder.HasData(
            new Circulation { Id = Guid.NewGuid(), Abbreviation = "PE", Description = "PROHIBICION ENTRADA" },
            new Circulation { Id = Guid.NewGuid(), Abbreviation = "PS", Description = "PROHIBICION SALIDA" },
            new Circulation { Id = Guid.NewGuid(), Abbreviation = "DET", Description = "DETENCION" },
            new Circulation { Id = Guid.NewGuid(), Abbreviation = "AE", Description = "AVISO ENTRADA" },
            new Circulation { Id = Guid.NewGuid(), Abbreviation = "AS", Description = "AVISO SALIDA" },
            new Circulation { Id = Guid.NewGuid(), Abbreviation = "DG", Description = "DROGAS" },
            new Circulation { Id = Guid.NewGuid(), Abbreviation = "PE", Description = "AVISO ENT/SAL" },
            new Circulation { Id = Guid.NewGuid(), Abbreviation = "PE", Description = "INTERES MIGRATORIO" },
            new Circulation { Id = Guid.NewGuid(), Abbreviation = "PE", Description = "PERDIDA DOCUMENTO" });
    }
}