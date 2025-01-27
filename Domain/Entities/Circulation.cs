using Domain.Commons;

namespace Domain.Entities;

public sealed class Circulation : BaseAuditableEntity
{
    public Guid Id { get; init; }

    // Número de expediente
    public required string FileNumber { get; init; } = GenerateFileNumber();

    // Primer nombre
    public required string FirstName { get; init; }

    // Segundo nombre
    public string SecondName { get; init; } = string.Empty;

    // Primer apellido
    public required string LastName1 { get; init; }

    // Segundo apellido
    public required string LastName2 { get; init; }

    // Fecha de nacimiento
    public DateTimeOffset BirthDate { get; init; }

    // Nacionalidad
    public Guid CitizenshipId { get; init; }
    public Citizenship? Citizenship { get; init; }

    // Circulación
    public Guid CirculationTypeId { get; init; }
    public CirculationType? CirculationType { get; init; }

    // Fecha de la circulación
    public DateTimeOffset CirculationDate { get; init; }

    // Fecha de vencimiento
    public Guid ExpirationId { get; init; }
    public Expiration? Expiration { get; init; }

    // Organo
    public Guid OrganId { get; init; }
    public Organ? Organ { get; init; }

    // Sección
    public required string Section { get; init; }

    // Oficial
    public required string Official { get; init; }

    // Teléfono 1
    public string Phone1 { get; init; } = string.Empty;

    // Teléfono 2
    public string Phone2 { get; init; } = string.Empty;

    // Instrucción
    public required string Instruction { get; init; }

    // Observaciones
    public string Observations { get; init; } = string.Empty;

    // Motivo de la circulación
    public string ReasonForCirculation { get; init; } = string.Empty;
    
    private static string GenerateFileNumber()
    {
        // Ejemplo: Exp-20250126160000-123e4567-e89b-12d3-a456-426614174000
        return $"Exp-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid()}";
    }
}