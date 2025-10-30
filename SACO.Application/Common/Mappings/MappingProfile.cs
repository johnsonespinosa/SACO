using AutoMapper;
using SACO.Domain.Entities;
using SACO.Shared.Models;

namespace SACO.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Passenger mappings
        CreateMap<Passenger, PassengerResponse>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));
        CreateMap<CreatePassengerRequest, Passenger>()
            .AfterMap((_, dest) => dest.CalculateSearchKey());

        // Circulation mappings
        CreateMap<Circulation, CirculationResponse>()
            .ForMember(dest => dest.PassengerFullName, opt => opt.MapFrom(src => src.Passenger.FullName))
            .ForMember(dest => dest.PassengerCitizenship, opt => opt.MapFrom(src => src.Passenger.Citizenship))
            .ForMember(dest => dest.PassengerBirthDate, opt => opt.MapFrom(src => src.Passenger.BirthDate))
            .ForMember(dest => dest.OrganName, opt => opt.MapFrom(src => src.Organ.Name))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));

        CreateMap<CreateCirculationRequest, Circulation>()
            .AfterMap((_, dest) => dest.CalculateTraceKey());
    }
}