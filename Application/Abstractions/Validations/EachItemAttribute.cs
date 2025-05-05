using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Application.Abstractions.Validations;

public class EachItemAttribute : ValidationAttribute
{
    public string RegularExpression { get; set; } = string.Empty;

    protected override ValidationResult IsValid(object? value, ValidationContext context)
    {
        if (value is IEnumerable<string> items)
        {
            var regex = new Regex(RegularExpression);
            if (items.Any(item => !regex.IsMatch(item)))
            {
                return new ValidationResult(ErrorMessage);
            }
        }
        return ValidationResult.Success!;
    }
}