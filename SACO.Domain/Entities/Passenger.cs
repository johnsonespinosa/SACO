using SACO.Domain.Common;

namespace SACO.Domain.Entities;

public class Passenger : BaseEntity
{
    // Personal information
    public string FirstName { get; set; } = string.Empty;
    public string? SecondName { get; set; }
    public string FirstLastName { get; set; } = string.Empty;
    public string? SecondLastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Citizenship { get; set; } = string.Empty; // Country of Citizenship
    
    // Support field for efficient searches - now with private setter
    public string SearchKey { get; private set; } = string.Empty;
    
    // Navigation properties
    public ICollection<Circulation> Circulations { get; set; } = new List<Circulation>();
    
    // Auxiliary methods (will be implemented in the Application layer)
    public string FullName => $"{FirstLastName} {SecondLastName ?? ""}, {FirstName} {SecondName ?? ""}".Trim();
    
    // Method to calculate and set SearchKey
    public void CalculateSearchKey()
    {
        SearchKey = $"{FirstLastName}{FirstName}{BirthDate:yyyyMMdd}".ToLower();
    }
    
    // Constructor to initialize SearchKey
    public Passenger()
    {
        CalculateSearchKey();
    }
}