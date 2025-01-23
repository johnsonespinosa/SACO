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

        builder.Property(circulationCode => circulationCode.Code)
            .HasMaxLength(4)
            .IsUnicode(false);

        builder.Property(circulationCode => circulationCode.Description)
            .HasMaxLength(25)
            .IsUnicode(false);

        builder.HasData(
            new Circulation { Id = Guid.NewGuid(), Code = "PE", Description = "PROHIBICION ENTRADA" },
            new Circulation { Id = Guid.NewGuid(), Code = "PS", Description = "PROHIBICION SALIDA" },
            new Circulation { Id = Guid.NewGuid(), Code = "DET", Description = "DETENCION" },
            new Circulation { Id = Guid.NewGuid(), Code = "AE", Description = "AVISO ENTRADA" },
            new Circulation { Id = Guid.NewGuid(), Code = "AS", Description = "AVISO SALIDA" },
            new Circulation { Id = Guid.NewGuid(), Code = "DG", Description = "DROGAS" },
            new Circulation { Id = Guid.NewGuid(), Code = "PE", Description = "AVISO ENT/SAL" },
            new Circulation { Id = Guid.NewGuid(), Code = "PE", Description = "INTERES MIGRATORIO" },
            new Circulation { Id = Guid.NewGuid(), Code = "PE", Description = "PERDIDA DOCUMENTO" });
    }
}