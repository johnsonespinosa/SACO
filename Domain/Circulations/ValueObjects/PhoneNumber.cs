using System.ComponentModel.DataAnnotations;

namespace Domain.Circulations.ValueObjects;

public record PhoneNumber([Required][RegularExpression(@"^\+?[1-9]\d{1,14}$")] string Number);