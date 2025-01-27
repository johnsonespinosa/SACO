using Shared.DTOs;
using Shared.DTOs.Users;
using Shared.Interfaces;

namespace Application.UseCases.Users.Commands.Update;

public record UpdateUserCommand(UserRequest UserRequest) : IRequest<ServiceResponse<ErrorOr<Unit>>>
{
    internal sealed class UpdateUserCommandHandler(IUserService service) : IRequestHandler<UpdateUserCommand, ServiceResponse<ErrorOr<Unit>>>
    {
        public async Task<ServiceResponse<ErrorOr<Unit>>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await service.UpdateAsync(request.UserRequest);
        }
    }
}