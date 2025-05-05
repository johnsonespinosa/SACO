using Application.Abstractions.DTOs;
using Application.Features.Commands.Circulations.Create;
using Application.Features.Commands.Circulations.Update;
using AutoMapper;
using Domain.Circulations;
using Domain.Circulations.Nomenclatures;
using Domain.Circulations.ValueObjects;

namespace Application.Abstractions.Mappings;

public class CirculationProfile : Profile
{
    public CirculationProfile()
    {
        // 1. Mapeo de Value Objects
        ConfigureValueObjectsMappings();
        
        // 2. Mapeo de Circulación
        ConfigureCirculationMappings();
        
        // 3. Mapeo de Comandos
        ConfigureCommandMappings();
        
        // 4. Mapeo de Nomencladores
        ConfigureNomenclatureMappings();
    }

    private void ConfigureValueObjectsMappings()
    {
        // PhoneNumber <-> string
        CreateMap<PhoneNumber, string>()
            .ConvertUsing(mappingExpression: phoneNumber => phoneNumber.Number);
            
        CreateMap<string, PhoneNumber>()
            .ConstructUsing(ctor: number => new PhoneNumber(number))
            .ForAllMembers(memberOptions: expression => expression.Ignore());

        // BirthDate <-> string
        CreateMap<string, BirthDate>()
            .ConstructUsing(ctor: dateString => new BirthDate(dateString))
            .ForAllMembers(memberOptions: expression => expression.Ignore());
    }

    private void ConfigureCirculationMappings()
    {
        // Circulation -> CirculationDto
        CreateMap<Circulation, CirculationDto>()
            .ForMember(destinationMember: dto => dto.CirculationId, memberOptions: expression =>
                expression.MapFrom(mapExpression: circulation => circulation.CirculationId))
            .ForMember(destinationMember: dto => dto.FirstName, memberOptions: expression =>
                expression.MapFrom(mapExpression: circulation => circulation.FirstName))
            .ForMember(destinationMember: dto => dto.SecondName, memberOptions: expression =>
                expression.MapFrom(mapExpression: circulation => circulation.SecondName))
            .ForMember(destinationMember: dto => dto.LastName1, memberOptions: expression =>
                expression.MapFrom(mapExpression: circulation => circulation.LastName1))
            .ForMember(destinationMember: dto => dto.LastName2, memberOptions: expression =>
                expression.MapFrom(mapExpression: circulation => circulation.LastName2))
            .ForMember(destinationMember: dto => dto.BirthDate, memberOptions: expression =>
                expression.MapFrom(mapExpression: circulation => circulation.BirthDate.FormattedDate))
            .ForMember(destinationMember: dto => dto.CitizenshipId, memberOptions: expression =>
                expression.MapFrom(mapExpression: circulation => circulation.CitizenshipId))
            .ForMember(destinationMember: dto => dto.Citizenship, memberOptions: expression =>
                expression.MapFrom(mapExpression: circulation => circulation.Citizenship))
            .ForMember(destinationMember: dto => dto.CirculationTypeId, memberOptions: expression =>
                expression.MapFrom(mapExpression: circulation => circulation.CirculationTypeId))
            .ForMember(destinationMember: dto => dto.CirculationType, memberOptions: expression =>
                expression.MapFrom(mapExpression: circulation => circulation.CirculationType))
            .ForMember(destinationMember: dto => dto.ExpirationId, memberOptions: expression =>
                expression.MapFrom(mapExpression: circulation => circulation.ExpirationId))
            .ForMember(destinationMember: dto => dto.Expiration, memberOptions: expression => 
                expression.MapFrom(mapExpression: circulation => circulation.Expiration))
            .ForMember(destinationMember: dto => dto.OrganId, memberOptions: expression => 
                expression.MapFrom(mapExpression: circulation => circulation.OrganId))
            .ForMember(destinationMember: dto => dto.Organ, memberOptions: expression => 
                expression.MapFrom(mapExpression: circulation => circulation.Organ))
            .ForMember(destinationMember: dto => dto.Section, memberOptions: expression => 
                expression.MapFrom(mapExpression: circulation => circulation.Section))
            .ForMember(destinationMember: dto => dto.Official, memberOptions: expression => 
                expression.MapFrom(mapExpression: circulation => circulation.Official))
            .ForMember(destinationMember: dto => dto.PhoneNumbers, memberOptions: expression => 
                expression.MapFrom(mapExpression: circulation => circulation.PhoneNumbers))
            .ForMember(destinationMember: dto => dto.Instruction, memberOptions: expression => 
                expression.MapFrom(mapExpression: circulation => circulation.Instruction))
            .ForMember(destinationMember: dto => dto.Observations, memberOptions: expression => 
                expression.MapFrom(mapExpression: circulation => circulation.Observations))
            .ForMember(destinationMember: dto => dto.ReasonForCirculation, memberOptions: 
                expression => expression.MapFrom(mapExpression: circulation => circulation.ReasonForCirculation));
    }

    private void ConfigureCommandMappings()
    {
        // CreateCirculationCommand -> Circulation
        CreateMap<CreateCirculationCommand, Circulation>()
            .ForMember(destinationMember: circulation => circulation.PhoneNumbers, memberOptions: expression =>
                expression.MapFrom(mapExpression: command => 
                command.PhoneNumbers.Select(number => new PhoneNumber(number)).ToList()))
            .ForMember(destinationMember: circulation => circulation.BirthDate, memberOptions: expression =>
                expression.MapFrom(mapExpression: command => new BirthDate(command.BirthDate)))
            .ForMember(destinationMember: circulation => circulation.Citizenship, memberOptions: expression =>
                expression.Ignore())
            .ForMember(destinationMember: circulation => circulation.CirculationType, memberOptions: expression =>
                expression.Ignore())
            .ForMember(destinationMember: circulation => circulation.Expiration, memberOptions: expression =>
                expression.Ignore())
            .ForMember(destinationMember: circulation => circulation.Organ, memberOptions: expression =>
                expression.Ignore())
            .AfterMap(afterFunction: (command, dest, _) => 
            {
                // Inicializar relaciones con IDs
                dest.Citizenship = new Citizenship { CitizenshipId = command.CitizenshipId };
                dest.CirculationType = new CirculationType { CirculationTypeId = command.CirculationTypeId };
                dest.Expiration = new Expiration { ExpirationId = command.ExpirationId };
                dest.Organ = new Organ { OrganId = command.OrganId };
            });

        // UpdateCirculationCommand -> Circulation
        CreateMap<UpdateCirculationCommand, Circulation>()
            .ForMember(destinationMember: circulation => circulation.CirculationId, memberOptions: expression =>
                expression.MapFrom(mapExpression: command => command.CirculationId))
            .ForMember(destinationMember: circulation => circulation.PhoneNumbers, memberOptions: expression =>
                expression.MapFrom(mapExpression: command => 
                command.PhoneNumbers.Select(number => new PhoneNumber(number)).ToList()))
            .ForMember(destinationMember: circulation => circulation.BirthDate, memberOptions: expression =>
                expression.MapFrom(mapExpression: command => new BirthDate(command.BirthDate)))
            .ForMember(destinationMember: circulation => circulation.Citizenship, memberOptions: expression =>
                expression.Ignore())
            .ForMember(destinationMember: circulation => circulation.CirculationType, memberOptions: expression =>
                expression.Ignore())
            .ForMember(destinationMember: circulation => circulation.Expiration, memberOptions: expression =>
                expression.Ignore())
            .ForMember(destinationMember: circulation => circulation.Organ, memberOptions: expression =>
                expression.Ignore())
            .AfterMap(afterFunction: (command, dest, _) => 
            {
                // Inicializar relaciones con IDs
                dest.Citizenship = new Citizenship { CitizenshipId = command.CitizenshipId };
                dest.CirculationType = new CirculationType { CirculationTypeId = command.CirculationTypeId };
                dest.Expiration = new Expiration { ExpirationId = command.ExpirationId };
                dest.Organ = new Organ { OrganId = command.OrganId };
            });
    }

    private void ConfigureNomenclatureMappings()
    {
        // Mapeo de nomencladores a sus DTOs
        CreateMap<Citizenship, CitizenshipDto>()
            .ForMember(destinationMember: dto => dto.CitizenshipId, memberOptions: expression => 
                expression.MapFrom(mapExpression: citizenship => citizenship.CitizenshipId))
            .ForMember(destinationMember: dto => dto.Abbreviation, memberOptions: expression =>
                expression.MapFrom(mapExpression: citizenship => citizenship.Abbreviation))
            .ForMember(destinationMember: dto => dto.Description, memberOptions: expression =>
                expression.MapFrom(mapExpression: citizenship => citizenship.Description));

        CreateMap<CirculationType, CirculationTypeDto>()
            .ForMember(destinationMember: dto => dto.CirculationTypeId, memberOptions: expression => 
                expression.MapFrom(mapExpression: circulationType => circulationType.CirculationTypeId))
            .ForMember(destinationMember: dto => dto.Abbreviation, memberOptions: expression => 
                expression.MapFrom(mapExpression: circulationType => circulationType.Abbreviation))
            .ForMember(destinationMember: dto => dto.Description, memberOptions: expression => 
                expression.MapFrom(mapExpression: circulationType => circulationType.Description));

        CreateMap<Expiration, ExpirationDto>()
            .ForMember(destinationMember: dto => dto.ExpirationId, memberOptions: expression =>
                expression.MapFrom(mapExpression: expiration => expiration.ExpirationId))
            .ForMember(destinationMember: dto => dto.Description, memberOptions: expression =>
                expression.MapFrom(mapExpression: expiration => expiration.Description));

        CreateMap<Organ, OrganDto>()
            .ForMember(destinationMember: dto => dto.OrganId, memberOptions: expression =>
                expression.MapFrom(mapExpression: organ => organ.OrganId))
            .ForMember(destinationMember: dto => dto.Name, memberOptions: expression =>
                expression.MapFrom(mapExpression: organ => organ.Name));
    }
}