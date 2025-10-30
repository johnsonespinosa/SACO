using SACO.Domain.Enums;

namespace SACO.Application.Models;

public class CurrentUserResponse
{
    public bool IsAuthenticated { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public UserType? UserType { get; set; }
    public IList<string> Roles { get; set; } = new List<string>();
}