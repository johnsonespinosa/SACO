using Domain.Entities;
using Shared.DTOs.Circulations;
using Shared.DTOs.Citizenships;
using Shared.DTOs.Expirations;
using Shared.DTOs.OperationalCirculations;
using Shared.DTOs.Organs;
using Shared.DTOs.Users;

namespace Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region OperationalCirculations
        // Mapeo desde CreateOperationalCirculationRequest a OperationalCirculation
        CreateMap<CirculationRequest, Circulation>()
            .ForMember(destinationMember: operationalCirculation =>  operationalCirculation.Id,
                memberOptions: opt
                    => opt.Ignore()) // Ignorar Id, ya que se genera automáticamente
            .ForMember(destinationMember: operationalCirculation => operationalCirculation.Created,
                memberOptions: opt
                    => opt.Ignore()) // Ignorar LastModified, ya que se genera automáticamente
            .ForMember(destinationMember: operationalCirculation => operationalCirculation.CreatedBy,
                memberOptions: opt
                    => opt.Ignore()) // Ignorar LastModifiedBy, ya que se genera automáticamente
            .ForMember(destinationMember: operationalCirculation => operationalCirculation.LastModified,
                memberOptions: opt
                    => opt.Ignore()) // Ignorar LastModified si es nullable
            .ForMember(destinationMember: operationalCirculation => operationalCirculation.LastModifiedBy,
                memberOptions: opt
                    => opt.Ignore()) // Ignorar LastModifiedBy si es nullable
            .ForMember(destinationMember: operationalCirculation => operationalCirculation.Deleted,
                 memberOptions: opt
                    => opt.Ignore()) // Ignorar Deleted si es nullable
            .ForMember(destinationMember: operationalCirculation => operationalCirculation.DeletedBy,
                memberOptions: opt
                    => opt.Ignore()); // Ignorar DeletedBy si es nullable

        
        // Mapeo desde UpdateOperationalCirculationRequest a OperationalCirculation
        CreateMap<CirculationRequest, Circulation>()
            .ForMember(destinationMember: operationalCirculation =>  operationalCirculation.Id,
                memberOptions: opt
                    => opt.Ignore()) // Ignorar Id, ya que se genera automáticamente
            .ForMember(destinationMember: operationalCirculation => operationalCirculation.Created,
                memberOptions: opt
                    => opt.Ignore()) // Ignorar Created si es nullable
            .ForMember(destinationMember: operationalCirculation => operationalCirculation.CreatedBy,
                memberOptions: opt
                    => opt.Ignore()) // Ignorar CreatedBy si es nullable
            .ForMember(destinationMember: operationalCirculation => operationalCirculation.LastModified,
                memberOptions: opt
                    => opt.Ignore()) // Ignorar LastModified, ya que se genera automáticamente
            .ForMember(destinationMember: operationalCirculation => operationalCirculation.LastModifiedBy,
                memberOptions: opt
                    => opt.Ignore()) // Ignorar LastModifiedBy, ya que se genera automáticamente
            .ForMember(destinationMember: operationalCirculation => operationalCirculation.Deleted,
                memberOptions: opt
                    => opt.Ignore()) // Ignorar Deleted si es nullable
            .ForMember(destinationMember: operationalCirculation => operationalCirculation.DeletedBy,
                memberOptions: opt
                    => opt.Ignore()); // Ignorar DeletedBy si es nullable
        
        // Mapeo desde OperationalCirculation a OperationalCirculationResponse
        CreateMap<Circulation, CirculationResponse>();
        #endregion

        #region Citizenships
        CreateMap<Citizenship, CitizenshipResponse>();
        #endregion

        #region Expirations
        CreateMap<Expiration, ExpirationResponse>();
        #endregion

        #region Organs
        CreateMap<Organ, OrganResponse>();
        #endregion

        #region Circulations
        CreateMap<CirculationType, CirculationTypeResponse>();
        #endregion

        #region Users

        CreateMap<UserRequest, User>()
            .ForMember(destinationMember: user =>  user.Id,
                memberOptions: opt
                    => opt.Ignore()) // Ignorar Id, ya que se genera automáticamente
            .ForMember(destinationMember: user => user.PasswordHash, memberOptions: opt
                => opt.MapFrom(mapExpression: createUserRequest => createUserRequest.Password));
        CreateMap<User, UserResponse>();

        #endregion
    }
}