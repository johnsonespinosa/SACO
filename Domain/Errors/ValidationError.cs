using Domain.Models;

namespace Domain.Errors;

public sealed record ValidationError(Error[] Errors)
    : Error("Validation.General",
        "Validation errors occurred",
        "One or more validation errors occurred",
        ErrorType.Validation)
{
    public static ValidationError FromResults(IEnumerable<Result> results)
        => new(Errors: results
            .Where(result => result.IsFailure)
            .SelectMany(result => result.Errors) // Usar SelectMany para aplanar
            .ToArray());
}