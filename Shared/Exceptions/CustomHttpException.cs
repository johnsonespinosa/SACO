namespace Shared.Exceptions;

public class CustomHttpException : Exception
{
    public string? ErrorContent { get; }

    public CustomHttpException(string message, string? errorContent) : base(message)
    {
        ErrorContent = errorContent;
    }

    public CustomHttpException(string message, Exception innerException) : base(message, innerException) { }
}