using System.ComponentModel.DataAnnotations;

namespace Application.Abstractions.Validations;

public class NotEmptyGuidAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext context)
        => (value is Guid guid && guid != Guid.Empty 
            ? ValidationResult.Success 
            : new ValidationResult($"{context.DisplayName} es requerido"))!;
}