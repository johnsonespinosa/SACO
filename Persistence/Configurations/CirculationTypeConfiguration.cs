using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CirculationTypeConfiguration : IEntityTypeConfiguration<CirculationType>
{
    public void Configure(EntityTypeBuilder<CirculationType> builder)
    {
        builder.ToTable("CirculationTypes");
        
        builder.HasKey(circulationType => circulationType.Id);


        builder.Property(circulationType => circulationType.Abbreviation)
            .HasMaxLength(4);

        builder.Property(circulationType => circulationType.Description)
            .HasMaxLength(25);

        builder.HasData(
            new CirculationType { Id = Guid.NewGuid(), Abbreviation = "PE", Description = "PROHIBICION ENTRADA" },
            new CirculationType { Id = Guid.NewGuid(), Abbreviation = "PS", Description = "PROHIBICION SALIDA" },
            new CirculationType { Id = Guid.NewGuid(), Abbreviation = "DET", Description = "DETENCION" },
            new CirculationType { Id = Guid.NewGuid(), Abbreviation = "AE", Description = "AVISO ENTRADA" },
            new CirculationType { Id = Guid.NewGuid(), Abbreviation = "AS", Description = "AVISO SALIDA" },
            new CirculationType { Id = Guid.NewGuid(), Abbreviation = "DG", Description = "DROGAS" },
            new CirculationType { Id = Guid.NewGuid(), Abbreviation = "PE", Description = "AVISO ENT/SAL" },
            new CirculationType { Id = Guid.NewGuid(), Abbreviation = "PE", Description = "INTERES MIGRATORIO" },
            new CirculationType { Id = Guid.NewGuid(), Abbreviation = "PE", Description = "PERDIDA DOCUMENTO" });
    }
}