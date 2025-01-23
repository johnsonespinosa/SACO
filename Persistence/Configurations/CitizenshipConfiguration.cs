using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CitizenshipConfiguration : IEntityTypeConfiguration<Citizenship>
{
    public void Configure(EntityTypeBuilder<Citizenship> builder)
    {
        builder.HasKey(citizenship => citizenship.Id);

        builder.ToTable("Citizenships");

        builder.Property(citizenship => citizenship.Abbreviation)
            .HasMaxLength(3)
            .IsUnicode(false);

        builder.Property(citizenship => citizenship.Description)
            .HasMaxLength(50)
            .IsUnicode(false);
        
        // Datos iniciales
        builder.HasData(
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "AFG", Description = "Afgano/a" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "DEU", Description = "Alemán/a" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "SAU", Description = "Árabe Saudita" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "ARG", Description = "Argentino/a" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "AUS", Description = "Australiano/a" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "BEL", Description = "Belga" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "BOL", Description = "Boliviano/a" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "BRA", Description = "Brasileño/a" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "KHM", Description = "Camboyano/a" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "CAN", Description = "Canadiense" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "CHL", Description = "Chileno/a" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "CHN", Description = "Chino/a" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "COL", Description = "Colombiano/a" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "KOR", Description = "Surcoreano/a" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "CRI", Description = "Costarricense" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "CUB", Description = "Cubano/a" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "DNK", Description = "Danés/danesa" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "ECU", Description = "Ecuatoriano/a" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "EGY", Description = "Egipcio/a" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "SLV", Description = "Salvadoreño/a" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "SCO", Description = "Escocés/escocesa" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "ESP", Description = "Español/a" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "USA", Description = "Estadounidense" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "EST", Description = "Estonio/a" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "ETH", Description = "Etíope" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "PHL", Description = "Filipino/a" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "FIN", Description = "Finlandés/finlandesa" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "FRA", Description = "Francés/francesa" },
            new Citizenship { Id = Guid.NewGuid(), Abbreviation = "WAL", Description ="Galés/galesa"},
            new Citizenship { Id= Guid.NewGuid(), Abbreviation="GRC",Description="Griego/a"},
            new Citizenship { Id= Guid.NewGuid(), Abbreviation="GTM",Description="Guatemalteco/a"},
            new Citizenship { Id= Guid.NewGuid(), Abbreviation="HTI",Description="Haitiano/a"},
            new Citizenship { Id= Guid.NewGuid(), Abbreviation="NLD",Description="Holandés/holandesa"},
            new Citizenship { Id= Guid.NewGuid(), Abbreviation="HND",Description="Hondureño/a"},
            new Citizenship { Id= Guid.NewGuid(), Abbreviation="MYS",Description="Malayo/malaya"},
            new Citizenship { Id= Guid.NewGuid(), Abbreviation="MAR",Description="Marroquí/marroquí"},
            new Citizenship { Id= Guid.NewGuid(), Abbreviation="MEX",Description="Mexicano/a"},
            new Citizenship { Id= Guid.NewGuid(), Abbreviation="NIC",Description="Nicaragüense"},
            new Citizenship { Id= Guid.NewGuid(), Abbreviation="NOR",Description="Noruego/noruega"},
            new Citizenship { Id= Guid.NewGuid(), Abbreviation="NZL",Description="Neozelandés/neozelandesa"},
            new Citizenship { Id= Guid.NewGuid(), Abbreviation="PAN",Description="Panameño/a"},
            new Citizenship { Id= Guid.NewGuid(), Abbreviation="PRY",Description="Paraguayo/a"},
            new Citizenship { Id= Guid.NewGuid(), Abbreviation="PER",Description="Peruano/a"},
            new Citizenship { Id= Guid.NewGuid(), Abbreviation="POL",Description="Polaco/polaca"},
            new Citizenship { Id= Guid.NewGuid(), Abbreviation="PRT",Description="Portugués/portuguesa"},
            new Citizenship { Id= Guid.NewGuid(), Abbreviation="PRI",Description="Puertorriqueño/puertorriqueña"},
            new Citizenship { Id= Guid.NewGuid(), Abbreviation="DOM",Description="Dominicano/dominicana"},
            new Citizenship { Id= Guid.NewGuid(), Abbreviation="ROU",Description="Rumano/rumana"},
            new Citizenship { Id= Guid.NewGuid(), Abbreviation="RUS",Description="Ruso/rusa"},
            new Citizenship { Id= Guid.NewGuid(), Abbreviation="SWE",Description="Sueco/sueca"}
        );
    }
}