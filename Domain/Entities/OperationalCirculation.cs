using Domain.Commons;

namespace Domain.Entities;

public sealed class OperationalCirculation : BaseAuditableEntity
{
    public Guid Id { get; init; }

    // Número de expediente
    public required string FileNumber { get; init; }

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

    // Ciudadanía
    public required string Citizenship { get; init; }

    // Circulación
    public required string CirculationType { get; init; }

    // Fecha de la circulación
    public DateTimeOffset CirculationDate { get; init; }

    // Fecha de vencimiento
    public DateTimeOffset ExpirationDate { get; init; }

    // Organo
    public required string Organ { get; init; }

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
}