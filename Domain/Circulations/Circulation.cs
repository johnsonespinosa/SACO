using Domain.Circulations.Nomenclatures;
using Domain.Circulations.ValueObjects;
using Domain.Commons;

namespace Domain.Circulations;

public sealed class Circulation(
    string firstName,
    string? secondName,
    string lastName1,
    string? lastName2,
    BirthDate birthDate,
    Guid citizenshipId,
    Guid circulationTypeId,
    Guid expirationId,
    Guid organId,
    string section,
    string official,
    List<PhoneNumber> phoneNumbers,
    string instruction,
    string observations,
    string reasonForCirculation)
    : AuditEntity
{
    public Guid CirculationId { get; } = Guid.NewGuid();
    public string FirstName { get; private init; } = firstName;
    public string? SecondName { get; private init; } = secondName;
    public string LastName1 { get; private init; } = lastName1;
    public string? LastName2 { get; private init; } = lastName2;
    public BirthDate BirthDate { get; private init; } = birthDate;
    public Guid CitizenshipId { get; private init; } = citizenshipId;
    public Citizenship Citizenship { get; set; } = null!;
    public Guid CirculationTypeId { get; private init; } = circulationTypeId;
    public CirculationType CirculationType { get; set; } = null!;
    public Guid ExpirationId { get; private init; } = expirationId;
    public Expiration Expiration { get; set; } = null!;
    public Guid OrganId { get; private init; } = organId;
    public Organ Organ { get; set; } = null!;
    public string Section { get; private init; } = section;
    public string Official { get; private init; } = official;
    public List<PhoneNumber> PhoneNumbers { get; private init; } = phoneNumbers;
    public string Instruction { get; private init; } = instruction;
    public string Observations { get; private init; } = observations;
    public string ReasonForCirculation { get; private init; } = reasonForCirculation;
}