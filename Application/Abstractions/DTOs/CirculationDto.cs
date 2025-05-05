using System.ComponentModel.DataAnnotations;
using Application.Abstractions.Validations;

namespace Application.Abstractions.DTOs;

public class CirculationDto
{
    public Guid CirculationId { get; set; }

    [Required(ErrorMessage = "El primer nombre es requerido.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "El primer nombre debe tener entre 1 y 50 caracteres.")]
    public string FirstName { get; set; } = string.Empty;

    [StringLength(50, ErrorMessage = "El segundo nombre debe tener hasta 50 caracteres.")]
    public string? SecondName { get; set; }

    [Required(ErrorMessage = "El primer apellido es requerido.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "El primer apellido debe tener entre 1 y 50 caracteres.")]
    public string LastName1 { get; set; } = string.Empty;

    [StringLength(50, ErrorMessage = "El segundo apellido debe tener hasta 50 caracteres.")]
    public string? LastName2 { get; set; }

    [Required]
    [RegularExpression(@"^\d{4}\.\d{2}\.\d{2}$")]
    public string BirthDate { get; set; } = string.Empty;

    [Required(ErrorMessage = "La ciudadanía es requerida.")]
    [NotEmptyGuid]
    public Guid CitizenshipId { get; set; }

    public CitizenshipDto Citizenship { get; set; }

    [Required(ErrorMessage = "La circulación es requerida.")]
    [NotEmptyGuid]
    public Guid CirculationTypeId { get; set; }

    public CirculationTypeDto CirculationType { get; set; }

    [Required(ErrorMessage = "La fecha de expiración es requerida.")]
    [NotEmptyGuid]
    public Guid ExpirationId { get; set; }

    public ExpirationDto Expiration { get; set; }

    [Required(ErrorMessage = "El órgano es requerida.")]
    [NotEmptyGuid]
    public Guid OrganId { get; set; }

    public OrganDto Organ { get; set; }

    [Required(ErrorMessage = "La sección es requerida.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "La sección debe tener entre 1 y 50 caracteres.")]
    public string Section { get; set; } = string.Empty;

    [Required(ErrorMessage = "El oficial es requerido.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "El oficial debe tener entre 1 y 100 caracteres.")]
    public string Official { get; set; } = string.Empty;

    [Required(ErrorMessage = "Al menos un número telefónico es requerido")]
    [MinLength(1, ErrorMessage = "Se requiere al menos un número telefónico")]
    [EachItem(RegularExpression = @"^\+?[1-9]\d{1,14}$", ErrorMessage = "Formato de teléfono inválido")]
    public List<string> PhoneNumbers { get; set; } = new();

    [Required(ErrorMessage = "La instrucción es requeridas.")]
    [StringLength(500, MinimumLength = 1, ErrorMessage = "La instrucción debe tener entre 1 y 500 caracteres.")]
    public string Instruction { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Las observaciones deben tener máximo 1000 caracteres")]
    public string Observations { get; set; } = string.Empty;

    [Required(ErrorMessage = "El motivo de la circulación es requerido.")]
    [StringLength(200, MinimumLength = 1,
        ErrorMessage = "El motivo de la circulación debe tener entre 1 y 200 caracteres.")]
    public string ReasonForCirculation { get; set; } = string.Empty;

    public string CirculationDate { get; set; } = string.Empty;
}