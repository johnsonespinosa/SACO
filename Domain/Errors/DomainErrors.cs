namespace Domain.Errors;

public static class DomainErrors
{
    public static class General
    {
        public static Error InvalidPageNumber => Error.Validation(
            code: "General.InvalidPageNumber",
            title: "Número de página inválido",
            detail: "El número de página debe ser mayor a 0");

        public static Error InvalidPageSize => Error.Validation(
            code: "General.InvalidPageSize",
            title: "Tamaño de página inválido",
            detail: "El tamaño de página debe estar entre 1 y 100");
        
        public static Error ServerError => Error.Internal(
            code: "General.ServerError",
            title: "Error del servidor",
            detail: "Ocurrió un error interno en el servidor");
    }
    public static class Circulation
    {
        public static Error InvalidBirthDate => Error.Validation(
            code: "Circulation.InvalidBirthDate",
            title: "Fecha inválida",
            detail: "El formato de fecha debe ser YYYY-MM-DD");
            
        public static Error NotFound => Error.NotFound(
            code: "Circulation.NotFound",
            title: "Circulación no encontrada",
            detail: "La circulación solicitada no existe");

        public static Error IdRequired => Error.Validation(
            code: "Circulation.IdRequired",
            title: "ID requerido",
            detail: "Se requiere el ID de la circulación");

        public static Error FirstNameRequired => Error.Validation(
            code: "Circulation.FirstNameRequired",
            title: "Nombre requerido",
            detail: "El nombre es obligatorio");

        public static Error FirstNameTooLong => Error.Validation(
            code: "Circulation.FirstNameTooLong",
            title: "Nombre muy largo",
            detail: "El nombre no puede exceder 50 caracteres");

        public static Error LastName1Required => Error.Validation(
            code: "Circulation.LastName1Required",
            title: "Apellido requerido",
            detail: "El primer apellido es obligatorio");

        public static Error CitizenshipNotFound => Error.Validation(
            code: "Circulation.CitizenshipNotFound",
            title: "Ciudadanía no encontrada",
            detail: "La ciudadanía seleccionada no existe");

        public static Error PhoneNumbersRequired => Error.Validation(
            code: "Circulation.PhoneNumbersRequired",
            title: "Teléfonos requeridos",
            detail: "Se requiere al menos un número telefónico");

        public static Error LastName1TooLong => Error.Validation(
            code: "Circulation.LastName1TooLong",
            title: "Apellido muy largo",
            detail: "El primer apellido no puede exceder 50 caracteres");

        public static Error CitizenshipRequired => Error.Validation(
            code: "Circulation.CitizenshipRequired",
            title: "Ciudadanía requerida",
            detail: "Se debe seleccionar una ciudadanía");

        public static Error CirculationTypeRequired => Error.Validation(
            code: "Circulation.CirculationTypeRequired",
            title: "Tipo de circulación requerido",
            detail: "Se debe seleccionar un tipo de circulación");

        public static Error CirculationTypeNotFound => Error.Validation(
            code: "Circulation.CirculationTypeNotFound",
            title: "Tipo de circulación no encontrado",
            detail: "El tipo de circulación seleccionado no existe");

        public static Error ExpirationRequired => Error.Validation(
            code: "Circulation.ExpirationRequired",
            title: "Expiración requerida",
            detail: "Se debe seleccionar una expiración");

        public static Error ExpirationNotFound => Error.Validation(
            code: "Circulation.ExpirationNotFound",
            title: "Expiración no encontrada",
            detail: "La expiración seleccionada no existe");

        public static Error OrganRequired => Error.Validation(
            code: "Circulation.OrganRequired",
            title: "Órgano requerido",
            detail: "Se debe seleccionar un órgano");

        public static Error OrganNotFound => Error.Validation(
            code: "Circulation.OrganNotFound",
            title: "Órgano no encontrado",
            detail: "El órgano seleccionado no existe");

        public static Error SectionRequired => Error.Validation(
            code: "Circulation.SectionRequired",
            title: "Sección requerida",
            detail: "La sección es obligatoria");

        public static Error SectionTooLong => Error.Validation(
            code: "Circulation.SectionTooLong",
            title: "Sección muy larga",
            detail: "La sección no puede exceder 100 caracteres");

        public static Error OfficialRequired => Error.Validation(
            code: "Circulation.OfficialRequired",
            title: "Oficial requerido",
            detail: "El nombre del oficial es obligatorio");

        public static Error OfficialTooLong => Error.Validation(
            code: "Circulation.OfficialTooLong",
            title: "Nombre de oficial muy largo",
            detail: "El nombre del oficial no puede exceder 100 caracteres");

        public static Error TooManyPhoneNumbers => Error.Validation(
            code: "Circulation.TooManyPhoneNumbers",
            title: "Demasiados teléfonos",
            detail: "Máximo 5 números telefónicos permitidos");

        public static Error InvalidPhoneNumberFormat => Error.Validation(
            code: "Circulation.InvalidPhoneNumberFormat",
            title: "Formato de teléfono inválido",
            detail: "Use formato internacional E.164: [+] [código país] [número]");

        public static Error InstructionTooLong => Error.Validation(
            code: "Circulation.InstructionTooLong",
            title: "Instrucciones muy largas",
            detail: "Las instrucciones no pueden exceder 500 caracteres");

        public static Error ObservationsTooLong => Error.Validation(
            code: "Circulation.ObservationsTooLong",
            title: "Observaciones muy largas",
            detail: "Las observaciones no pueden exceder 1000 caracteres");

        public static Error ReasonRequired => Error.Validation(
            code: "Circulation.ReasonRequired",
            title: "Motivo requerido",
            detail: "El motivo de la circulación es obligatorio");

        public static Error ReasonTooLong => Error.Validation(
            code: "Circulation.ReasonTooLong",
            title: "Motivo muy largo",
            detail: "El motivo no puede exceder 200 caracteres");
    }
    public static class Auth
    {
        public static Error InvalidToken => Error.Validation(
            code: "Auth.InvalidToken",
            title: "Token inválido",
            detail: "El token de acceso proporcionado es inválido o está corrupto");

        public static Error UserNotFound => Error.NotFound(
            code: "Auth.UserNotFound",
            title: "Usuario no encontrado",
            detail: "No existe un usuario asociado a este token");
        public static Error IpAddressRequired => Error.Validation(
            code: "Auth.IpAddressRequired",
            title: "Dirección IP requerida",
            detail: "La dirección IP del cliente es obligatoria");

        public static Error InvalidIpFormat => Error.Validation(
            code: "Auth.InvalidIpFormat",
            title: "Formato de IP inválido",
            detail: "La dirección IP debe ser IPv4 o IPv6 válida");
        
        public static Error EmailNotConfirmed => Error.Validation(
            code: "Auth.EmailNotConfirmed",
            title: "Email no confirmado",
            detail: "Debes confirmar tu correo electrónico antes de iniciar sesión");

        public static Error ServerError => Error.Internal(
            code: "Auth.ServerError",
            title: "Error interno",
            detail: "Ocurrió un error durante el proceso de autenticación");
        public static Error InvalidCredentials => Error.Unauthorized(
            code: "Auth.InvalidCredentials",
            title: "Credenciales inválidas",
            detail: "Usuario o contraseña incorrectos");

        public static Error AccountLocked => Error.Conflict(
            code: "Auth.AccountLocked",
            title: "Cuenta bloqueada",
            detail: "La cuenta está temporalmente bloqueada por múltiples intentos fallidos");

        public static Error InvalidRefreshToken => Error.Validation(
            code: "Auth.InvalidRefreshToken",
            title: "Refresh token inválido",
            detail: "El refresh token proporcionado es inválido o ha expirado");
    }
    public static class User
    {
        public static Error UpdateFailed(IEnumerable<Error> errors) => 
            new Error(
                Code: "User.UpdateFailed",
                Title: "Error actualizando usuario",
                Detail: string.Join(", ", errors.Select(error => error.Detail)),
                Type: ErrorType.Validation);
        public static Error IdRequired => Error.Validation(
            code: "User.IdRequired",
            title: "ID requerido",
            detail: "Se requiere el ID del usuario");

        public static Error DeleteFailed => Error.Internal(
            code: "User.DeleteFailed",
            title: "Error de eliminación",
            detail: "No se pudo eliminar el usuario");
         public static Error UsernameRequired => Error.Validation(
        code: "User.UsernameRequired",
        title: "Usuario requerido",
        detail: "El nombre de usuario es obligatorio");

    public static Error UsernameTooShort => Error.Validation(
        code: "User.UsernameTooShort",
        title: "Usuario muy corto",
        detail: "El nombre de usuario debe tener al menos 3 caracteres");

    public static Error UsernameTooLong => Error.Validation(
        code: "User.UsernameTooLong",
        title: "Usuario muy largo",
        detail: "El nombre de usuario no puede exceder 50 caracteres");

    public static Error EmailRequired => Error.Validation(
        code: "User.EmailRequired",
        title: "Email requerido",
        detail: "El correo electrónico es obligatorio");

    public static Error InvalidEmailFormat => Error.Validation(
        code: "User.InvalidEmailFormat",
        title: "Formato de email inválido",
        detail: "El formato del correo electrónico es incorrecto");

    public static Error PhoneRequired => Error.Validation(
        code: "User.PhoneRequired",
        title: "Teléfono requerido",
        detail: "El número de teléfono es obligatorio");

    public static Error InvalidPhoneFormat => Error.Validation(
        code: "User.InvalidPhoneFormat",
        title: "Formato de teléfono inválido",
        detail: "Use formato internacional E.164: [+] [código país] [número]");

    public static Error PasswordRequired => Error.Validation(
        code: "User.PasswordRequired",
        title: "Contraseña requerida",
        detail: "La contraseña es obligatoria");

    public static Error PasswordTooShort => Error.Validation(
        code: "User.PasswordTooShort",
        title: "Contraseña muy corta",
        detail: "La contraseña debe tener al menos 8 caracteres");

    public static Error PasswordMissingUppercase => Error.Validation(
        code: "User.PasswordMissingUppercase",
        title: "Mayúscula requerida",
        detail: "La contraseña debe contener al menos una letra mayúscula");

    public static Error PasswordMissingLowercase => Error.Validation(
        code: "User.PasswordMissingLowercase",
        title: "Minúscula requerida",
        detail: "La contraseña debe contener al menos una letra minúscula");

    public static Error PasswordMissingNumber => Error.Validation(
        code: "User.PasswordMissingNumber",
        title: "Número requerido",
        detail: "La contraseña debe contener al menos un número");
        public static Error PasswordsDoNotMatch => Error.Validation(
            code: "User.PasswordsDoNotMatch",
            title: "Contraseña no coincidente",
            detail: "La contraseña y la contraseña de confirmación no coinciden");

        public static Error CreationFailed => Error.Internal(
            code: "User.CreationFailed",
            title: "Falló la creación del usuario",
            detail: "Ocurrió un error al crear el usuario.");

        public static Error RoleAssignmentFailed => Error.Internal(
            code: "User.RoleAssignmentFailed",
            title: "Asignación de rol fallida",
            detail: "No se pudo asignar el rol al usuario");
        
        public static Error EmailAlreadyExists => Error.Conflict(
            code: "User.EmailConflict",
            title: "Email ya registrado",
            detail: "El correo electrónico proporcionado ya está en uso");
        
        public static Error UsernameAlreadyExists => Error.Conflict(
            code: "User.UsernameConflict",
            title: "Username ya registrado",
            detail: "El nombre de usuario ya está en uso");
        
        public static Error NotFound => Error.NotFound(
            code: "User.NotFound",
            title: "Usuario no encontrado",
            detail: "El usuario solicitado no existe");
        
        public static Error CurrentPasswordRequired => Error.Validation(
            code: "User.CurrentPasswordRequired",
            title: "Contraseña actual requerida",
            detail: "Se requiere la contraseña actual para actualizar la contraseña");
            
        public static Error InvalidCurrentPassword => Error.Unauthorized(
            code: "User.InvalidCurrentPassword",
            title: "Contraseña actual incorrecta",
            detail: "La contraseña actual no coincide con nuestros registros");
    }
}