using Application.DTOs.Users;
using Application.Interfaces;

namespace Application.UseCases.Users.Commands.Create;

public record CreateUserCommand(CreateUserRequest UserRequest) : IRequest<ErrorOr<Unit>>
{
    internal sealed class CreateUserCommandHandler(IUserService service) : IRequestHandler<CreateUserCommand, ErrorOr<Unit>>
    {
        public async Task<ErrorOr<Unit>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await service.AddAsync(request.UserRequest);
        }
    }
}