using Shared.DTOs;
using Shared.DTOs.Users;
using Shared.Interfaces;

namespace Application.UseCases.Users.Commands.Create;

public record CreateUserCommand(UserRequest UserRequest) : IRequest<ServiceResponse<ErrorOr<Unit>>>
{
    internal sealed class CreateUserCommandHandler(IUserService service) : IRequestHandler<CreateUserCommand, ServiceResponse<ErrorOr<Unit>>>
    {
        public async Task<ServiceResponse<ErrorOr<Unit>>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await service.AddAsync(request.UserRequest);
        }
    }
}