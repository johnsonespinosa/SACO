namespace Domain.Errors;

public enum ErrorType
{
    None = 0,
    InvalidArgument = 1,
    Validation = 2,
    NotFound = 3,
    Conflict = 4,
    Unauthenticated = 5,
    PermissionDenied = 6,
    Internal = 7
}