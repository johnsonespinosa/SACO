using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class OrganConfiguration : IEntityTypeConfiguration<Organ>
{
    public void Configure(EntityTypeBuilder<Organ> builder)
    {
        builder.ToTable("Organs");

        builder.Property(organ => organ.Id);

        builder.Property(organ => organ.Name).HasMaxLength(10);

        builder.HasData(
            new Organ { Id = Guid.NewGuid(), Name = "PTI"},
            new Organ { Id = Guid.NewGuid(), Name = "DNA"},
            new Organ { Id = Guid.NewGuid(), Name = "DGI"},
            new Organ { Id = Guid.NewGuid(), Name = "DGCI"},
            new Organ { Id = Guid.NewGuid(), Name = "PNR"},
            new Organ { Id = Guid.NewGuid(), Name = "DCIM"},
            new Organ { Id = Guid.NewGuid(), Name = "DICO"},
            new Organ { Id = Guid.NewGuid(), Name = "CII"},
            new Organ { Id = Guid.NewGuid(), Name = "DSP"},
            new Organ { Id = Guid.NewGuid(), Name = "JEF-AR"},
            new Organ { Id = Guid.NewGuid(), Name = "JEF-MB"},
            new Organ { Id = Guid.NewGuid(), Name = "JEF-PR"},
            new Organ { Id = Guid.NewGuid(), Name = "JEF-MA"},
            new Organ { Id = Guid.NewGuid(), Name = "JEF-VC"},
            new Organ { Id = Guid.NewGuid(), Name = "JEF-CF"},
            new Organ { Id = Guid.NewGuid(), Name = "JEF-SS"},
            new Organ { Id = Guid.NewGuid(), Name = "JEF-AV"},
            new Organ { Id = Guid.NewGuid(), Name = "JEF-CM"},
            new Organ { Id = Guid.NewGuid(), Name = "JEF-GR"},
            new Organ { Id = Guid.NewGuid(), Name = "JEF-GU"},
            new Organ { Id = Guid.NewGuid(), Name = "JEF-TU"},
            new Organ { Id = Guid.NewGuid(), Name = "JEF-ME"},
            new Organ { Id = Guid.NewGuid(), Name = "JEF-ME" },
            new Organ { Id = Guid.NewGuid(), Name = "JEF-HO" },
            new Organ { Id = Guid.NewGuid(), Name = "JEF-SC" },
            new Organ { Id = Guid.NewGuid(), Name = "PNR"},
            new Organ { Id = Guid.NewGuid(), Name = "FGR"},
            new Organ { Id = Guid.NewGuid(), Name = "TSP"},
            new Organ { Id = Guid.NewGuid(), Name = "FMTAR"});
    }
}