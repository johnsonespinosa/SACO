using Shared.DTOs;
using Shared.DTOs.Users;
using Shared.Interfaces;

namespace Application.UseCases.Users.Commands.Authenticate;

public record AuthenticateUserCommand(AuthenticationRequest AuthenticationRequest, string IpAddress) : IRequest<ServiceResponse<AuthenticationResponse>>
{
    internal sealed class AuthenticateUserCommandHandler(IUserService service) : IRequestHandler<AuthenticateUserCommand, ServiceResponse<AuthenticationResponse>>
    {
        public async Task<ServiceResponse<AuthenticationResponse>> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            return await service.AuthenticateAsync(request: request.AuthenticationRequest, request.IpAddress);
        }
    }
}