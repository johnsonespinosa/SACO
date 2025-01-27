using Shared.DTOs;
using Shared.DTOs.Organs;
using Shared.Interfaces;

namespace Application.UseCases.Organs.Queries.GetAll;

public record GetAllOrgansQuery : IRequest<ServiceResponse<IReadOnlyCollection<OrganResponse>>>
{
    internal sealed class GetAllOrgansQueryHandler(IOrganService service) : IRequestHandler<GetAllOrgansQuery, ServiceResponse<IReadOnlyCollection<OrganResponse>>>
    {
        public async Task<ServiceResponse<IReadOnlyCollection<OrganResponse>>> Handle(GetAllOrgansQuery request, CancellationToken cancellationToken)
        {
            return await service.GetAll();
        }
    }
}