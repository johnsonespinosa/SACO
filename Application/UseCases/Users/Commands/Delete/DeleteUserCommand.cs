using Shared.DTOs;
using Shared.Interfaces;

namespace Application.UseCases.Users.Commands.Delete;

public record DeleteUserCommand(string Id) : IRequest<ServiceResponse<ErrorOr<Unit>>>
{
    internal sealed class DeleteUserCommandHandler(IUserService service) : IRequestHandler<DeleteUserCommand, ServiceResponse<ErrorOr<Unit>>>
    {
        public async Task<ServiceResponse<ErrorOr<Unit>>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await service.DeleteAsync(request.Id);
        }
    }
}