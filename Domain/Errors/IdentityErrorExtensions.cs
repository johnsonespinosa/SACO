using Microsoft.AspNetCore.Identity;

namespace Domain.Errors;

public static class IdentityErrorExtensions
{
    public static IEnumerable<Error> ToApplicationErrors(
        this IEnumerable<IdentityError> identityErrors)
    {
        return identityErrors.Select(error => Error.Validation(
            code: error.Code,
            title: "Error de Identidad",
            detail: error.Description));
    }
}