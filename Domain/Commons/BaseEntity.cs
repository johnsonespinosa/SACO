using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Commons;

/// <summary>
/// Representa una raíz agregada en el dominio, que es un concepto clave en la arquitectura limpia.
/// Un agregado es un objeto que encapsula toda la lógica de negocios relacionada con un concepto específico,
/// manteniendo su estado interno y permitiendo operaciones como crear, leer, actualizar y eliminar (CRUD).
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Lista interna que almacena los eventos del dominio generados por las operaciones realizadas en esta raíz agregada.
    /// Los eventos son objetos que representan cambios o acciones importantes dentro del sistema del dominio.
    /// </summary>
    private readonly List<DomainEvent> _domainEvents = [];

    /// <summary>
    /// Propiedad pública que expone la lista de eventos del dominio almacenados en la raíz del agregado.
    /// Permite el acceso a los eventos generados sin modificarlos directamente.
    /// </summary>
    [NotMapped]
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Método protegido que permite generar un nuevo evento de dominio y agregarlo a la lista de eventos.
    /// Este método se utiliza para registrar eventos que ocurren durante las operaciones comerciales.
    /// </summary>
    /// <param name="domainEvent">El evento de dominio que se agregará a la lista.</param>
    public void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}