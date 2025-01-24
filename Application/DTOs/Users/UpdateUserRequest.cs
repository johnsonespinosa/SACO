namespace Application.DTOs.Users;

public class UpdateUserRequest : CreateUserRequest
{
    public string? Id { get; init; }
}