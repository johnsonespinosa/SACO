using Application.Abstractions.Interfaces.Messaging;

namespace Application.Features.Commands.Users.CreateUser;

public record CreateUserCommand(
    string UserName,
    string Email,
    string PhoneNumber,
    string Password,
    string PasswordConfirm) : ICommand<string>;

