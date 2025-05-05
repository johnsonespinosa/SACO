namespace Application.Abstractions.DTOs;

public class PaginationDto
{
    public string? Filter { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public PaginationDto()
    {
        Filter = "";
        PageNumber = 1;
        PageSize = 10;
    }

    public PaginationDto(int pageNumber, int pageSize)
    {
        Filter = "";
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize > 10 ? 10 : pageSize;
    }
}