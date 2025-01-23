namespace Application.Security;

/// <summary>
/// Especifica que la clase a la que se aplica este atributo requiere autorización.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public abstract class AuthorizeAttribute : Attribute
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="AuthorizeAttribute"/>.
    /// </summary>
    protected AuthorizeAttribute() { }

    /// <summary>
    /// Obtiene o establece una lista delimitada por comas de roles que tienen permiso para acceder al recurso.
    /// </summary>
    public string Roles { get; set; } = string.Empty;

    /// <summary>
    /// Obtiene o establece el nombre de la política que determina el acceso al recurso.
    /// </summary>
    public string Policy { get; set; } = string.Empty;
}