using Application.Abstractions.DTOs;
using Application.Abstractions.Interfaces.Messaging;

namespace Application.Features.Commands.Authentication;

public record RefreshTokenCommand(
    string AccessToken,
    string RefreshToken,
    string IpAddress) : ICommand<AuthResponse>;