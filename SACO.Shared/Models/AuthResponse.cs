using SACO.Domain.Enums;

namespace SACO.Shared.Models;

public class AuthResponse
{
    public bool Success { get; set; }
    public Guid? UserId { get; set; }
    public string? UserName { get; set; }
    public UserType? UserType { get; set; }
    public string[] Errors { get; set; } = [];
}