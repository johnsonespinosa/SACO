namespace Shared.DTOs;

public record ServiceResponse<T>(T? Result, bool Succeeded, string Message);
