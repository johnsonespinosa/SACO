namespace Application.Models;

public class ErrorResponse(List<Error>? errors, bool isError) : IErrorOr
{
    // Propiedad para almacenar los errores
    private List<Error>? Errors { get; } = errors;

    // Propiedad para indicar si hay un error
    public bool IsError { get; } = isError;

    // Implementación de la interfaz IErrorOr
    List<Error>? IErrorOr.Errors => Errors;
}