using System.Collections.ObjectModel;

namespace Domain.Models;

/// <summary>
/// Representa un resultado paginado de una consulta con metadatos de paginación
/// </summary>
/// <typeparam name="T">Tipo de los elementos en la colección</typeparam>
public sealed record PaginatedResult<T>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public int TotalItems { get; init; }
    public int TotalPages { get; init; }
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
    public IReadOnlyList<T> Items { get; init; }

    private PaginatedResult(
        IReadOnlyList<T> items,
        int totalItems,
        int pageNumber,
        int pageSize)
    {
        ArgumentNullException.ThrowIfNull(items);
        if (pageNumber < 1) throw new ArgumentOutOfRangeException(nameof(pageNumber));
        if (pageSize < 1) throw new ArgumentOutOfRangeException(nameof(pageSize));
        if (totalItems < 0) throw new ArgumentOutOfRangeException(nameof(totalItems));
        if (items.Count > pageSize) throw new ArgumentException("Items count exceeds page size");

        // Conversión segura a IList<T>
        var itemsList = items.ToList();
        Items = new ReadOnlyCollection<T>(itemsList);
    
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalItems = totalItems;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
    }

    /// <summary>
    /// Factory method para crear una instancia válida
    /// </summary>
    public static PaginatedResult<T> Create(
        IReadOnlyList<T> items,
        int totalItems,
        int pageNumber,
        int pageSize)
    {
        return new PaginatedResult<T>(items, totalItems, pageNumber, pageSize);
    }

    /// <summary>
    /// Resultado vacío (patrón Null Object)
    /// </summary>
    public static PaginatedResult<T> Empty(int pageSize = 10)
        => new(Array.Empty<T>(), 0, 1, pageSize);

    /// <summary>
    /// Mapea los items a otro tipo preservando la paginación
    /// </summary>
    public PaginatedResult<TResult> Map<TResult>(Func<T, TResult> mapFunction)
        => new(
            Items.Select(mapFunction).ToList(),
            TotalItems,
            PageNumber,
            PageSize);

    /// <summary>
    /// Deconstructor para pattern matching
    /// </summary>
    public void Deconstruct(
        out int pageNumber,
        out int pageSize,
        out int totalItems,
        out IReadOnlyList<T> items)
    {
        pageNumber = PageNumber;
        pageSize = PageSize;
        totalItems = TotalItems;
        items = Items;
    }

    /// <summary>
    /// Representación textual para debugging
    /// </summary>
    public override string ToString()
        => $"Page {PageNumber} of {TotalPages} ({Items.Count} items of {TotalItems})";
}