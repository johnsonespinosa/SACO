using Application.DTOs.Users;
using Application.Interfaces;

namespace Application.UseCases.Users.Queries.GetAll;

public record GetAllUsersQuery : IRequest<IReadOnlyCollection<UserResponse>>
{
    internal sealed class GetAllUsersQueryHandler(IUserService service) : IRequestHandler<GetAllUsersQuery, IReadOnlyCollection<UserResponse>>
    {
        public async Task<IReadOnlyCollection<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await service.GetAllAsync();
        }
    }
}