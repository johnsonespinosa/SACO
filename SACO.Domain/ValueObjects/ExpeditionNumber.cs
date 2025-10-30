namespace SACO.Domain.ValueObjects;

public class ExpeditionNumber
{
    public string Value { get; }
    public string Prefix { get; }
    public int Number { get; }
    
    // Public contractor for EF
    public ExpeditionNumber() {} // Parameterless constructor for EF
    
    private ExpeditionNumber(string value, string prefix, int number)
    {
        Value = value;
        Prefix = prefix;
        Number = number;
    }
    
    public static ExpeditionNumber CreateOperative(int sequence)
    {
        return new ExpeditionNumber($"OP-{sequence:D6}", "OP", sequence);
    }
    
    public static ExpeditionNumber CreateEffective(int sequence)
    {
        return new ExpeditionNumber($"EF-{sequence:D6}", "EF", sequence);
    }
    
    public static ExpeditionNumber Parse(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Expedition number cannot be empty");
            
        var parts = value.Split('-');
        if (parts.Length != 2 || !int.TryParse(parts[1], out var number))
            throw new ArgumentException("Invalid expedition number format");
            
        return new ExpeditionNumber(value, parts[0], number);
    }
    
    public override string ToString() => Value;
    
    // For equality
    public override bool Equals(object? obj)
    {
        if (obj is ExpeditionNumber other)
            return Value == other.Value;
        return false;
    }
    
    public override int GetHashCode() => Value.GetHashCode();
}