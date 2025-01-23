namespace Application.Models;

public class ServiceResponse<T>
{
    public bool Succeeded { get; init; }
    public string? Message { get; init; }
    public IList<Error> Errors { get; init; }
    public T? Data { get; init; }

    // Constructor privado para éxito
    private ServiceResponse(T? data, string message)
    {
        Data = data;
        Message = message;
        Succeeded = true;
        Errors = new List<Error>(); 
    }

    // Constructor privado para fallo
    private ServiceResponse(IList<Error>? errors, string message)
    {
        Errors = errors ?? new List<Error>();
        Message = message;
        Succeeded = false;
    }

    // Método estático para respuesta exitosa
    public static ServiceResponse<T> Successful(string message, T data)
    {
        return new ServiceResponse<T>(data, message);
    }

    // Método estático para respuesta fallida
    public static ServiceResponse<T> Failure(IList<Error>? errors, string message)
    {
        return new ServiceResponse<T>(errors, message);
    }
}