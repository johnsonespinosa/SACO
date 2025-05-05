namespace Domain.Models;

public class TokenValidationResult
{
    public bool IsValid { get; set; }
    public DateTime Expiration { get; set; }
    public string? Error { get; set; }
}