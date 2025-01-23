using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class OperationalCirculationConfiguration : IEntityTypeConfiguration<OperationalCirculation>
    {
        public void Configure(EntityTypeBuilder<OperationalCirculation> builder)
        {
            // Configuración de la tabla
            builder.ToTable("OperationalCirculations");

            // Configuración de las propiedades
            builder.HasKey(operationalCirculation => operationalCirculation.Id); 

            builder.HasIndex(operationalCirculation => operationalCirculation.BirthDate, name: "OperationalCirculation_BirthDate");

            builder.Property(operationalCirculation => operationalCirculation.FileNumber)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .ValueGeneratedOnAdd();

            builder.Property(operationalCirculation => operationalCirculation.LastName1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedOnAdd();

            builder.Property(operationalCirculation => operationalCirculation.LastName2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

            builder.Property(operationalCirculation => operationalCirculation.CirculationType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

            builder.Property(operationalCirculation => operationalCirculation.Citizenship)
                    .HasMaxLength(20)
                    .IsUnicode(false);

            builder.Property(operationalCirculation => operationalCirculation.CirculationDate)
                    .HasMaxLength(6)
                    .IsUnicode(false);

            builder.Property(operationalCirculation => operationalCirculation.BirthDate)
                    .HasMaxLength(6)
                    .IsUnicode(false);

            builder.Property(operationalCirculation => operationalCirculation.ExpirationDate)
                    .HasMaxLength(6)
                    .IsUnicode(false);

            builder.Property(operationalCirculation => operationalCirculation.Instruction)
                    .HasMaxLength(40)
                    .IsUnicode(false);

            builder.Property(operationalCirculation => operationalCirculation.ReasonForCirculation)
                    .HasMaxLength(500)
                    .IsUnicode(false);

            builder.Property(operationalCirculation => operationalCirculation.FirstName)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .ValueGeneratedOnAdd();

            builder.Property(operationalCirculation => operationalCirculation.SecondName)
                    .HasMaxLength(12)
                    .IsUnicode(false);

            builder.Property(operationalCirculation => operationalCirculation.Observations)
                    .HasMaxLength(40)
                    .IsUnicode(false);

            builder.Property(operationalCirculation => operationalCirculation.Official)
                    .HasMaxLength(30)
                    .IsUnicode(false);

            builder.Property(operationalCirculation => operationalCirculation.Organ)
                    .HasMaxLength(30)
                    .IsUnicode(false);

            builder.Property(operationalCirculation => operationalCirculation.Section)
                    .HasMaxLength(30)
                    .IsUnicode(false);

            builder.Property(operationalCirculation => operationalCirculation.Phone1)
                    .HasMaxLength(8)
                    .IsUnicode(false);

            builder.Property(operationalCirculation => operationalCirculation.Phone2)
                    .HasMaxLength(8)
                    .IsUnicode(false);
                
            // Configuración de las propiedades de auditoría
            builder.Property(operationalCirculation => operationalCirculation.CreatedBy).HasMaxLength(50);
            
            builder.Property(operationalCirculation => operationalCirculation.Created).IsRequired();
            
            builder.Property(operationalCirculation => operationalCirculation.LastModifiedBy).HasMaxLength(50);
            
            builder.Property(operationalCirculation => operationalCirculation.LastModified);
        }
    }