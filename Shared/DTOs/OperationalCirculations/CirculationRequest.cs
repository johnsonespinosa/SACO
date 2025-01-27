using Shared.DTOs.Circulations;
using Shared.DTOs.Citizenships;
using Shared.DTOs.Expirations;
using Shared.DTOs.Organs;

namespace Shared.DTOs.OperationalCirculations;

public class CirculationRequest
{
    public Guid Id { get; set; }
    // Primer nombre
    public string FirstName { get; set; } = string.Empty;

    // Segundo nombre
    public string SecondName { get; set; } = string.Empty;

    // Primer apellido
    public string LastName1 { get; set; } = string.Empty;

    // Segundo apellido
    public string LastName2 { get; set; } = string.Empty;

    // Fecha de nacimiento
    public DateTimeOffset BirthDate { get; set; }

    // Ciudadanía
    public CitizenshipResponse? Citizenship { get; set; }

    // Circulación
    public CirculationTypeResponse? CirculationType { get; set; }

    // Fecha de la circulación
    public DateTimeOffset CirculationDate { get; init; }

    // Fecha de vencimiento
    public ExpirationResponse? Expiration { get; set; }

    // Organo
    public OrganResponse? Organ { get; set; }

    // Sección
    public string Section { get; set; } = string.Empty;

    // Oficial
    public string Official { get; set; } = string.Empty;

    // Teléfono 1
    public string Phone1 { get; set; } = string.Empty;

    // Teléfono 2
    public string Phone2 { get; set; } = string.Empty;

    // Instrucción
    public string Instruction { get; set; } = string.Empty;

    // Observaciones
    public string Observations { get; set; } = string.Empty;

    // Motivo de la circulación
    public string ReasonForCirculation { get; set; } = string.Empty;
}