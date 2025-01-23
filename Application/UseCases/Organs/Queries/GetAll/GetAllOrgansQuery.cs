using Application.DTOs.Organs;
using Application.Interfaces;

namespace Application.UseCases.Organs.Queries.GetAll;

public record GetAllOrgansQuery : IRequest<IReadOnlyCollection<OrganResponse>>
{
    internal sealed class GetAllOrgansQueryHandler(IOrganService service) : IRequestHandler<GetAllOrgansQuery, IReadOnlyCollection<OrganResponse>>
    {
        public async Task<IReadOnlyCollection<OrganResponse>> Handle(GetAllOrgansQuery request, CancellationToken cancellationToken)
        {
            return await service.GetAll();
        }
    }
}