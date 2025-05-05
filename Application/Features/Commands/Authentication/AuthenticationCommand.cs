using Application.Abstractions.DTOs;
using Application.Abstractions.Interfaces.Messaging;

namespace Application.Features.Commands.Authentication;

public record AuthenticationCommand(string UserName, string Password, string IpAddress) : ICommand<AuthResponse>;

