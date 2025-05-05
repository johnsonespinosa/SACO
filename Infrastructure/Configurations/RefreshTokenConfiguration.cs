using Domain.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable(name: "REFRESH_TOKENS", schema: "SYSTEM");
        
        // Llave primaria
        builder.HasKey(refreshToken => refreshToken.Id)
            .HasName("PK_REFRESH_TOKENS");
        
        builder.Property(refreshToken => refreshToken.Id)
            .HasColumnName("REFRESH_TOKEN_ID")
            .HasDefaultValueSql("SYS_GUID()");

        // Relación con User (1:N)
        builder.Property(refreshToken => refreshToken.UserId)
            .HasColumnName("USER_ID")
            .IsRequired()
            .HasColumnType("NVARCHAR2(450)"); // Tamaño compatible con IdentityUser.CirculationId
        
        builder.HasOne<Domain.Users.User>()
            .WithMany()
            .HasForeignKey(refreshToken => refreshToken.UserId)
            .HasConstraintName("FK_REFRESH_TOKENS_USERS");

        // Propiedades principales
        builder.Property(refreshToken => refreshToken.Token)
            .HasColumnName("TOKEN")
            .IsRequired()
            .HasColumnType("NVARCHAR2(128)")
            .HasMaxLength(128);

        builder.Property(refreshToken => refreshToken.Expire)
            .HasColumnName("EXPIRE_AT")
            .HasColumnType("TIMESTAMP(7) WITH TIME ZONE")
            .IsRequired();

        builder.Property(refreshToken => refreshToken.Created)
            .HasColumnName("CREATED_AT")
            .HasColumnType("TIMESTAMP(7) WITH TIME ZONE")
            .IsRequired()
            .HasDefaultValueSql("SYSTIMESTAMP");

        builder.Property(refreshToken => refreshToken.CreatedByIp)
            .HasColumnName("CREATED_BY_IP")
            .IsRequired()
            .HasColumnType("NVARCHAR2(45)"); // Tamaño para IPv6
        
        // Propiedades de revocación
        builder.Property(refreshToken => refreshToken.Revoked)
            .HasColumnName("REVOKED_AT")
            .HasColumnType("TIMESTAMP(7) WITH TIME ZONE");

        builder.Property(refreshToken => refreshToken.RevokedByIp)
            .HasColumnName("REVOKED_BY_IP")
            .HasColumnType("NVARCHAR2(45)");

        builder.Property(refreshToken => refreshToken.ReplacedByToken)
            .HasColumnName("REPLACED_BY_TOKEN")
            .HasColumnType("NVARCHAR2(128)");

        // Índices
        builder.HasIndex(refreshToken => refreshToken.Token)
            .IsUnique()
            .HasDatabaseName("IDX_REFRESH_TOKENS_TOKEN");

        builder.HasIndex(refreshToken => refreshToken.UserId)
            .HasDatabaseName("IDX_REFRESH_TOKENS_USER");

        builder.HasIndex(refreshToken => refreshToken.Expire)
            .HasDatabaseName("IDX_REFRESH_TOKENS_EXPIRE");

        // Ignorar propiedades calculadas
        builder.Ignore(refreshToken => refreshToken.IsExpired);
        builder.Ignore(refreshToken => refreshToken.IsActive);
        builder.Ignore(refreshToken => refreshToken.IsRevoked);
    }
}