using Domain.Circulations.Nomenclatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ExpirationConfiguration : IEntityTypeConfiguration<Expiration>
{
    public void Configure(EntityTypeBuilder<Expiration> builder)
    {
        builder.ToTable(name: "EXPIRATIONS", schema: "SYSTEM");
        
        builder.HasKey(expiration => expiration.ExpirationId)
            .HasName("PK_EXPIRATIONS");

        builder.Property(expiration => expiration.ExpirationId)
            .HasColumnName("EXPIRATION_ID")
            .HasDefaultValueSql("SYS_GUID()");

        builder.Property(expiration => expiration.Description)
            .HasColumnName("DESCRIPTION")
            .HasMaxLength(100)
            .IsRequired();
    }
}