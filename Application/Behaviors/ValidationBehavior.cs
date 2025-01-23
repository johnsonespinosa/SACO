using Application.Models;

namespace Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Si no hay validador, continúa con el siguiente manejador
        if (validator is null)
            return await next();

        // Realiza la validación
        var validationResults = await validator.ValidateAsync(request, cancellationToken);

        // Si la validación es válida, continúa con el siguiente manejador
        if (validationResults.IsValid)
            return await next();

        // Convierte los errores de validación a un formato compatible con TResponse
        var errors = validationResults.Errors.ConvertAll(validationFailure =>
            Error.Validation(code: validationFailure.PropertyName, description: validationFailure.ErrorMessage));

        // Aquí asumimos que TResponse tiene un constructor o método para aceptar errores.
        return CreateErrorResponse(errors);
    }

    private TResponse CreateErrorResponse(List<Error> errors)
    {
        // Crea una instancia de ErrorResponse con los errores proporcionados.
        var errorResponse = new ErrorResponse(errors, isError: true);
        
        // Asegúrate de que TResponse sea compatible con ErrorResponse.
        return (TResponse)(object)errorResponse;
    }
}