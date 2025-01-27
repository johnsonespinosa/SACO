using Shared.DTOs;
using Shared.DTOs.Users;
using Shared.Interfaces;

namespace Application.UseCases.Users.Queries.GetAll;

public record GetAllUsersQuery(string FilterRequest) : IRequest<ServiceResponse<IReadOnlyCollection<UserResponse>>>
{
    internal sealed class GetAllUsersQueryHandler(IUserService service) : IRequestHandler<GetAllUsersQuery, ServiceResponse<IReadOnlyCollection<UserResponse>>>
    {
        public async Task<ServiceResponse<IReadOnlyCollection<UserResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await service.GetAllAsync(request.FilterRequest);
        }
    }
}