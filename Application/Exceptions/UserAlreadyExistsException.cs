namespace Application.Exceptions;

public class UserAlreadyExistsException(string value)
    : Exception($"Ya existe un usuario con el nombre o el email: {value}");