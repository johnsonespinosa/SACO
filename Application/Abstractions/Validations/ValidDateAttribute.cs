using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Application.Abstractions.Validations;

public class ValidDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext context)
    {
        if (value is string dateString && 
            DateTime.TryParseExact(dateString, "yyyy.MM.dd", 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
        {
            return ValidationResult.Success!;
        }
        return new ValidationResult(ErrorMessage ?? "Fecha inválida");
    }
}