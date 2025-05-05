namespace Domain.Errors;

public record Error(string Code, string Title, string Detail, ErrorType Type)
{
    public static readonly Error None = new(string.Empty, string.Empty, string.Empty, ErrorType.None);
    public static readonly Error NullValue = new(
        Code: "General.NullValue",
        Title: "Null value",
        Detail: "A null value was provided",
        ErrorType.InvalidArgument);

    public static Error Validation(string code, string title, string detail) =>
        new(code, title, detail, ErrorType.Validation);

    public static Error NotFound(string code, string title, string detail) =>
        new(code, title, detail, ErrorType.NotFound);

    public static Error Conflict(string code, string title, string detail) =>
        new(code, title, detail, ErrorType.Conflict);

    public static Error Unauthorized(string code, string title, string detail) =>
        new(code, title, detail, ErrorType.Unauthenticated);

    public static Error Forbidden(string code, string title, string detail) =>
        new(code, title, detail, ErrorType.PermissionDenied);

    public static Error Internal(string code, string title, string detail)
        => new(code, title, detail, ErrorType.Internal);
}