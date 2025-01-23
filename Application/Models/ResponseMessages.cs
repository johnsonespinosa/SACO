namespace Application.Models;

public abstract class ResponseMessages
{
    // Mensajes para operaciones CRUD
    public const string CreateSuccess = "El registro se ha creado exitosamente.";
    public const string CreateFailure = "Error al crear el registro. Por favor, inténtelo de nuevo.";
    
    public const string ReadSuccess = "Los datos se han recuperado exitosamente.";
    public const string ReadNotFound = "El registro solicitado no fue encontrado.";
    
    public const string UpdateSuccess = "El registro se ha actualizado exitosamente.";
    public const string UpdateFailure = "Error al actualizar el registro. Por favor, inténtelo de nuevo.";
    
    public const string DeleteSuccess = "El registro se ha eliminado exitosamente.";
    public const string DeleteFailure = "Error al eliminar el registro. Por favor, inténtelo de nuevo.";

    // Mensajes para autenticación
    public const string AuthSuccess = "Autenticación exitosa.";
    public const string AuthFailure = "Credenciales inválidas. Por favor, verifique su usuario y contraseña.";
    
    // Mensajes específicos para búsqueda de usuario por correo electrónico
    public const string UserNotFoundByEmail = "No hay ninguna cuenta registrada con el correo electrónico: {0}.";
    
    // Mensajes específicos para inicio de sesión
    public const string InvalidCredentials = "Las credenciales no son válidas para el usuario: {0}.";
    
    // Mensajes para autorización
    public const string AuthorizationSuccess = "Acceso autorizado.";
    public const string AuthorizationFailure = "No tiene permiso para realizar esta acción.";

    // Mensajes generales
    public const string OperationSuccess = "La operación se ha realizado con éxito.";
    public const string OperationFailure = "Ocurrió un error al realizar la operación. Por favor, inténtelo de nuevo más tarde.";

    // Mensajes de existencia
    public const string ResourceExists = "El recurso ya existe.";
    public const string ResourceDoesNotExist = "El recurso no existe.";

    // Mensajes de validación
    public const string ValidationError = "Se encontraron errores de validación. Por favor, revise los datos ingresados.";

    // Mensajes de sistema
    public const string SystemError = "Ocurrió un error en el sistema. Por favor, contacte al soporte técnico.";
}