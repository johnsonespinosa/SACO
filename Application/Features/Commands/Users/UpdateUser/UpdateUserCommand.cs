using Application.Abstractions.Interfaces.Messaging;

namespace Application.Features.Commands.Users.UpdateUser;

public record UpdateUserCommand(
    string Id,
    string UserName,
    string Email,
    string PhoneNumber,
    string CurrentPassword,
    string NewPassword) : ICommand;