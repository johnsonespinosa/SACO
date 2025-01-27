namespace Shared.DTOs.OperationalCirculations;

public class CirculationResponse
{
    public Guid Id { get; init; }
    // Primer nombre
    public required string FirstName { get; init; }
    
    // Primer apellido
    public required string LastName1 { get; init; }

    // Segundo apellido
    public required string LastName2 { get; init; }
    
    // Fecha de nacimiento
    public DateTimeOffset BirthDate { get; init; }
    
    // Circulación
    public required string CirculationType { get; init; }
}