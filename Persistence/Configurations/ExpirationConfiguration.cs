using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ExpirationConfiguration : IEntityTypeConfiguration<Expiration>
{
    public void Configure(EntityTypeBuilder<Expiration> builder)
    {
        builder.ToTable("Expirations");

        builder.Property(expirationCode => expirationCode.Id);

        builder.Property(expirationCode => expirationCode.Description).HasMaxLength(3);

        builder.HasData(new Expiration
            {
                Id = Guid.NewGuid(),
                Description = "1"
            },
            new Expiration
            {
                Id = Guid.NewGuid(),
                Description = "21"
            },
            new Expiration
            {
                Id = Guid.NewGuid(),
                Description = "90"
            });
    }
}