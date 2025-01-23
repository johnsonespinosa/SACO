using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CitizenshipConfiguration : IEntityTypeConfiguration<Citizenship>
{
    public void Configure(EntityTypeBuilder<Citizenship> builder)
    {
        builder.HasKey(countryCode => countryCode.NumericCode)
            .HasName("SYS_C001162978");

        builder.ToTable("CODPAIS");

        // builder.Property(countryCode => countryCode.NumericCode)
        //     // .HasColumnType("NUMBER")
        //     .HasColumnName("CODN");

        builder.Property(countryCode => countryCode.Code)
            .HasMaxLength(3)
            .IsUnicode(false);

        builder.Property(countryCode => countryCode.Description)
            .HasMaxLength(50)
            .IsUnicode(false);
    }
}