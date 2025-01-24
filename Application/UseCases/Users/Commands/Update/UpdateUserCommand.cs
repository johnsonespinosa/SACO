using Application.DTOs.Users;
using Application.Interfaces;

namespace Application.UseCases.Users.Commands.Update;

public record UpdateUserCommand(UpdateUserRequest UserRequest) : IRequest<ErrorOr<Unit>>
{
    internal sealed class UpdateUserCommandHandler(IUserService service) : IRequestHandler<UpdateUserCommand, ErrorOr<Unit>>
    {
        public async Task<ErrorOr<Unit>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await service.UpdateAsync(request.UserRequest);
        }
    }
}