using Application.DTOs.Users;
using Application.Interfaces;

namespace Application.UseCases.Users.Commands.Authenticate;

public record AuthenticateUserCommand(AuthenticationRequest AuthenticationRequest, string IpAddress) : IRequest<AuthenticationResponse>
{
    internal sealed class AuthenticateUserCommandHandler(IUserService service) : IRequestHandler<AuthenticateUserCommand, AuthenticationResponse>
    {
        public async Task<AuthenticationResponse> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            return await service.AuthenticateAsync(request: request.AuthenticationRequest, request.IpAddress);
        }
    }
}