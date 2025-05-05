using System.ComponentModel.DataAnnotations;

namespace Application.Abstractions.DTOs;

public class AuthRequest
{
    [Required(ErrorMessage = "El nombre de usuario es requerido")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre de usuario debe tener entre 3 y 50 caracteres")]
    public string UserName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "La contraseña es requerida")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", 
        ErrorMessage = "La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula y un número")]
    public string Password { get; set; } = string.Empty;
}