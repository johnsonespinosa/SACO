using Application.Abstractions.Interfaces.Messaging;

namespace Application.Features.Commands.Users.DeleteUser;

public record DeleteUserCommand(string Id) : ICommand;