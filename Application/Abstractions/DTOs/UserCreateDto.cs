using System.ComponentModel.DataAnnotations;

namespace Application.Abstractions.DTOs;

public class UserCreateDto
{
    [Required(ErrorMessage = "El nombre de usuario es requerido")]
    [StringLength(50, MinimumLength = 3, 
        ErrorMessage = "El nombre de usuario debe tener entre 3 y 50 caracteres")]
    public string UserName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El email es requerido")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    [StringLength(100, ErrorMessage = "El email no puede exceder 100 caracteres")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El número telefónico es requerido")]
    [RegularExpression(@"^\+[1-9]\d{1,14}$", 
        ErrorMessage = "Formato E.164 requerido: [+][código país][número]")]
    public string PhoneNumber { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "La contraseña es requerida")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", 
        ErrorMessage = "Mínimo 8 caracteres con mayúscula, minúscula y número")]
    public string Password { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "La confirmación de contraseña es requerida")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
    public string PasswordConfirm { get; set; } = string.Empty;
}