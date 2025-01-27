namespace Shared.DTOs.Users;

public class UserRequest
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string PasswordConfirm { get; set; } = string.Empty;
}