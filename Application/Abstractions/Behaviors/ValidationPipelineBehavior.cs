using System.Reflection;
using Domain.Errors;
using Domain.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.Abstractions.Behaviors;

internal sealed class ValidationPipelineBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var failures = new List<ValidationFailure>();

        foreach (var validator in validators)
        {
            var result = await validator.ValidateAsync(context, cancellationToken);
            failures.AddRange(result.Errors);
        }

        if (failures.Count != 0)
            return HandleValidationErrors(failures);

        return await next(cancellationToken);
    }

    private static TResponse HandleValidationErrors(IEnumerable<ValidationFailure> failures)
    {
        // 1. Corregir la construcción de errores
        var errors = failures
            .GroupBy(validationFailure => validationFailure.PropertyName)
            .Select(validationFailures => Error.Validation(
                code: $"Validation.{validationFailures.Key}",
                title: "Validation Error",
                detail: string.Join("; ", validationFailures.Select(validationFailure => validationFailure.ErrorMessage))))
            .ToArray();

        var validationError = new ValidationError(errors);

        // 2. Manejar correctamente Result y Result<T>
        if (typeof(TResponse) == typeof(Result))
        {
            return (TResponse)Result.Failure(validationError);
        }

        // 3. Usar reflexión de manera segura para Result<T>
        var resultType = typeof(TResponse).GetGenericArguments()[0];
        var failureMethod = typeof(Result<>)
            .MakeGenericType(resultType)
            .GetMethod(name: "Failure", bindingAttr: BindingFlags.Public | BindingFlags.Static)!;

        return (TResponse)failureMethod.Invoke(obj: null, [validationError])!;
    }
}