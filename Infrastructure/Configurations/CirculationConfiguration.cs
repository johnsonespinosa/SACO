using Domain.Circulations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CirculationConfiguration : IEntityTypeConfiguration<Circulation>
{
    public void Configure(EntityTypeBuilder<Circulation> builder)
    {
        // Tabla y esquema
        builder.ToTable(name: "CIRCULATIONS", schema: "SYSTEM");
        
        // Llave primaria
        builder.HasKey(circulation => circulation.CirculationId)
            .HasName("PK_CIRCULATIONS");
        
        // Propiedades
        builder.Property(circulation => circulation.CirculationId)
            .HasColumnName("CIRCULATION_ID")
            .HasDefaultValueSql("SYS_GUID()");

        builder.Property(circulation => circulation.FirstName)
            .HasColumnName("FIRST_NAME")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(circulation => circulation.SecondName)
            .HasColumnName("SECOND_NAME")
            .HasMaxLength(50);

        builder.Property(circulation => circulation.LastName1)
            .HasColumnName("LAST_NAME1")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(circulation => circulation.LastName2)
            .HasColumnName("LAST_NAME2")
            .HasMaxLength(50);

        // Value Object: BirthDate
        builder.OwnsOne(navigationExpression: circulation => circulation.BirthDate, buildAction: navigationBuilder =>
        {
            navigationBuilder.Property(birthDate => birthDate.FormattedDate)
                .HasColumnName("BIRTH_DATE")
                .HasMaxLength(10)
                .IsFixedLength()
                .IsRequired();
            
            navigationBuilder.Ignore(birthDate => birthDate.Date);
        });

        // Relaciones
        builder.HasOne(navigationExpression: circulation => circulation.Citizenship)
            .WithMany(navigationExpression: citizenship => citizenship.Circulations)
            .HasForeignKey(circulation => circulation.CitizenshipId)
            .HasConstraintName("FK_CIRCULATIONS_CITIZENSHIP");

        builder.HasOne(navigationExpression: circulation => circulation.CirculationType)
            .WithMany(navigationExpression: circulationType => circulationType.Circulations)
            .HasForeignKey(circulation => circulation.CirculationTypeId)
            .HasConstraintName("FK_CIRCULATIONS_CIRCULATION_TYPE");

        builder.HasOne(navigationExpression: circulation => circulation.Expiration)
            .WithMany(navigationExpression: expiration => expiration.Circulations)
            .HasForeignKey(circulation => circulation.ExpirationId)
            .HasConstraintName("FK_CIRCULATIONS_EXPIRATION");

        builder.HasOne(navigationExpression: circulation => circulation.Organ)
            .WithMany(navigationExpression: organ => organ.Circulations)
            .HasForeignKey(circulation => circulation.OrganId)
            .HasConstraintName("FK_CIRCULATIONS_ORGAN");

        // Value Objects: PhoneNumbers (tabla separada)
        builder.OwnsMany(navigationExpression: circulation => circulation.PhoneNumbers, navigationBuilder =>
        {
            navigationBuilder.ToTable(name: "CIRCULATION_PHONES", schema: "SYSTEM");
            
            navigationBuilder.WithOwner().HasForeignKey("CIRCULATION_ID");
            
            navigationBuilder.Property(phoneNumber => phoneNumber.Number)
                .HasColumnName("PHONE_NUMBER")
                .HasMaxLength(15)
                .IsRequired();
            
            navigationBuilder.HasKey("CIRCULATION_ID", "PHONE_NUMBER");
        });

        // Configuración de columnas
        builder.Property(circulation => circulation.Section)
            .HasColumnName("SECTION")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(circulation => circulation.Official)
            .HasColumnName("OFFICIAL")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(circulation => circulation.Instruction)
            .HasColumnName("INSTRUCTION")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(circulation => circulation.Observations)
            .HasColumnName("OBSERVATIONS")
            .HasMaxLength(1000);

        builder.Property(circulation => circulation.ReasonForCirculation)
            .HasColumnName("REASON")
            .HasMaxLength(200)
            .IsRequired();
        
        // Configuración de propiedades de auditoría
        builder.Property(circulation => circulation.CreatedAt)
            .HasColumnName("CREATED_AT")
            .HasColumnType("TIMESTAMP(7)")
            .IsRequired()
            .HasDefaultValueSql("SYSTIMESTAMP");

        builder.Property(circulation => circulation.CreatedBy)
            .HasColumnName("CREATED_BY")
            .HasColumnType("RAW(16)")
            .HasMaxLength(450);

        builder.Property(circulation => circulation.UpdatedAt)
            .HasColumnName("UPDATED_AT")
            .HasColumnType("TIMESTAMP(7)");

        builder.Property(circulation => circulation.UpdatedBy)
            .HasColumnName("UPDATED_BY")
            .HasColumnType("RAW(16)")
            .HasMaxLength(450);

        builder.Property(circulation => circulation.IsDeleted)
            .HasColumnName("IS_DELETED")
            .HasColumnType("NUMBER(1)")
            .HasDefaultValue(0)
            .IsRequired()
            .HasConversion(
                v => v ? 1 : 0,  // A Oracle
                v => v != 0      // Desde Oracle
            );

        // 2. Filtro para eliminación lógica
        builder.HasQueryFilter(circulation => !circulation.IsDeleted);

        // 3. Índices para búsquedas frecuentes
        builder.HasIndex(circulation => new { circulation.FirstName, circulation.LastName1 })
            .HasDatabaseName("IDX_CIRCULATIONS_NAME")
            .IsUnique(false);

        builder.HasIndex(circulation => circulation.BirthDate.FormattedDate)
            .HasDatabaseName("IDX_CIRCULATIONS_BIRTHDATE")
            .IsUnique(false);
    }
}