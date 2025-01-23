namespace Application.Interfaces;

public interface IApplicationLogger<T>
{
    void LogInformation(string message);
    void LogWarning(string message);
    void LogError(Exception exception, string message);
}