using AutoMapper;
using SACO.Application.Models;
using SACO.Domain.Entities;

namespace SACO.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Passenger mappings
        CreateMap<Passenger, PassengerDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));
        CreateMap<CreatePassengerDto, Passenger>()
            .AfterMap((_, dest) => dest.CalculateSearchKey());

        // Circulation mappings
        CreateMap<Circulation, CirculationDto>()
            .ForMember(dest => dest.PassengerFullName, opt => opt.MapFrom(src => src.Passenger.FullName))
            .ForMember(dest => dest.PassengerCitizenship, opt => opt.MapFrom(src => src.Passenger.Citizenship))
            .ForMember(dest => dest.PassengerBirthDate, opt => opt.MapFrom(src => src.Passenger.BirthDate))
            .ForMember(dest => dest.OrganName, opt => opt.MapFrom(src => src.Organ.Name))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));

        CreateMap<CreateCirculationDto, Circulation>()
            .AfterMap((_, dest) => dest.CalculateTraceKey());
    }
}