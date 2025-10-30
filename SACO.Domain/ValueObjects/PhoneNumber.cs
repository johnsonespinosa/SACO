namespace SACO.Domain.ValueObjects;

public record PhoneNumber
{
    public string Value { get; }
    
    // Private constructor to force the use of the factory method
    private PhoneNumber(string value) => Value = value;
    
    // Factory method to create valid instances
    public static PhoneNumber Create(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number cannot be empty");
            
        var cleaned = new string(phoneNumber.Where(char.IsDigit).ToArray());
        if (cleaned.Length < 7)
            throw new ArgumentException("Phone number is too short");
            
        return new PhoneNumber(cleaned);
    }
    
    // To allow null in Entity Framework
    public static PhoneNumber? CreateNullable(string? phoneNumber)
    {
        return string.IsNullOrWhiteSpace(phoneNumber) ? null : Create(phoneNumber);
    }
    
    // For serialization/deserialization
    public override string ToString() => Value;
    
    // Implicit conversion to string for ease of use
    public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.Value;
    
    // Remove all the manually defined operators and equality methods
    // The record type handles these automatically
}