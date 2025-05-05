using System.Diagnostics.CodeAnalysis;
using Domain.Errors;

namespace Domain.Models;

public class Result
{
    protected Result(bool isSuccess, IEnumerable<Error> errors)
    {
        var errorList = errors.ToList();
        var hasErrors = errorList.Count != 0;

        if ((isSuccess && hasErrors) || (!isSuccess && !hasErrors))
            throw new ArgumentException(message: "Estado de error no válido");

        IsSuccess = isSuccess;
        Errors = errorList;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public List<Error> Errors { get; }

    public static Result Success() => new(isSuccess: true, errors: Enumerable.Empty<Error>());
    public static Result<TValue> Success<TValue>(TValue value) 
        => new(value, isSuccess: true, errors: Enumerable.Empty<Error>());

    public static Result Failure(Error error) => new(isSuccess: false, errors: new[] { error });
    public static Result Failure(IEnumerable<Error> errors) => new(isSuccess: false, errors);
    public static Result<TValue> Failure<TValue>(Error error) 
        => new(value: default, isSuccess: false, errors: new[] { error });
    
    public static Result<TValue> Failure<TValue>(IEnumerable<Error> errors) 
        => new(value: default, isSuccess: false, errors);
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess,  IEnumerable<Error> errors) : base(isSuccess, errors)
    {
        _value = value;
    }

    [MemberNotNullWhen(returnValue: true, member: nameof(_value))]
    public new bool IsSuccess => base.IsSuccess;

    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException(message: "No se puede acceder al valor del resultado fallido");

    public static implicit operator Result<TValue>(TValue? value)
    {
        return value is not null
            ? Success(value)
            : Failure<TValue>(Error.NullValue);
    }
}