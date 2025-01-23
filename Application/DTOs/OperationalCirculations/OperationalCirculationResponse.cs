namespace Application.DTOs.OperationalCirculations;

public class OperationalCirculationResponse
{
    // Primer nombre
    public required string FirstName { get; init; }
    
    // Primer apellido
    public required string LastName1 { get; init; }

    // Segundo apellido
    public required string LastName2 { get; init; }
    
    // Circulación
    public required string CirculationType { get; init; }
}