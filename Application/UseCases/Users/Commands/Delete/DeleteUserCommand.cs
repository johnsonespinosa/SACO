using Application.Interfaces;

namespace Application.UseCases.Users.Commands.Delete;

public record DeleteUserCommand(string Id) : IRequest<ErrorOr<Unit>>
{
    internal sealed class DeleteUserCommandHandler(IUserService service) : IRequestHandler<DeleteUserCommand, ErrorOr<Unit>>
    {
        public async Task<ErrorOr<Unit>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await service.DeleteAsync(request.Id);
        }
    }
}