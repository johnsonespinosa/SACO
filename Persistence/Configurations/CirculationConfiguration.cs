using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CirculationConfiguration : IEntityTypeConfiguration<Circulation>
{
    public void Configure(EntityTypeBuilder<Circulation> builder)
    {
        // Configuración de la tabla
        builder.ToTable("Circulations");

        // Configuración de las propiedades
        builder.HasKey(circulation => circulation.Id);

        builder.Property(circulation => circulation.FileNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(circulation => circulation.FirstName)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.HasIndex(circulation => circulation.FirstName);

        builder.Property(circulation => circulation.SecondName)
            .HasMaxLength(50);

        builder.Property(circulation => circulation.LastName1)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.HasIndex(circulation => circulation.LastName1);

        builder.Property(circulation => circulation.LastName2)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.HasIndex(circulation => circulation.LastName2);

        builder.Property(circulation => circulation.BirthDate)
            .IsRequired();
        
        builder.HasIndex(circulation => circulation.BirthDate);

        builder.Property(circulation => circulation.CitizenshipId)
            .IsRequired();

        builder.Property(circulation => circulation.CirculationTypeId)
            .IsRequired();

        builder.Property(circulation => circulation.CirculationDate)
            .IsRequired();

        builder.Property(circulation => circulation.ExpirationId)
            .IsRequired();

        builder.Property(circulation => circulation.OrganId)
            .IsRequired();

        builder.Property(circulation => circulation.Section)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(circulation => circulation.Official)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(circulation => circulation.Phone1)
            .HasMaxLength(15); 

        builder.Property(circulation => circulation.Phone2)
            .HasMaxLength(15); 

        builder.Property(circulation => circulation.Instruction)
            .IsRequired()
            .HasMaxLength(250); 

        builder.Property(circulation => circulation.Observations)
            .HasMaxLength(500); 

        builder.Property(circulation => circulation.ReasonForCirculation)
            .HasMaxLength(250);

        // Configuración de las relaciones
        builder.HasOne(navigationExpression: circulation => circulation.Citizenship)
            .WithMany(navigationExpression: citizenship => citizenship.Circulations)
            .HasForeignKey(circulation => circulation.CitizenshipId);

        builder.HasOne(navigationExpression: circulation => circulation.CirculationType)
            .WithMany(navigationExpression: circulationType => circulationType.Circulations) 
            .HasForeignKey(circulation => circulation.CirculationTypeId);

        builder.HasOne(navigationExpression: circulation => circulation.Expiration)
            .WithMany(navigationExpression: expiration => expiration.Circulations) 
            .HasForeignKey(circulation => circulation.ExpirationId);

        builder.HasOne(navigationExpression: circulation => circulation.Organ)
            .WithMany(navigationExpression: organ => organ.Circulations) 
            .HasForeignKey(circulation => circulation.OrganId);
    }
}