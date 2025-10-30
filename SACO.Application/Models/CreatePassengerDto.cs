namespace SACO.Application.Models;

public class CreatePassengerDto
{
    public string FirstName { get; set; } = string.Empty;
    public string? SecondName { get; set; }
    public string FirstLastName { get; set; } = string.Empty;
    public string? SecondLastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Citizenship { get; set; } = string.Empty;
}