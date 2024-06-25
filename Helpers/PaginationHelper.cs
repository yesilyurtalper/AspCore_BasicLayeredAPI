namespace BasicLayeredService.API.Helpers;

public class PagedResponse<T>
{
    public int TotalItems { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public bool HasNext { get; set; }
    public bool HasPrevious { get; set; }
    public List<T> Items { get; set; }
}

public static class PaginationHelper
{
    public static PagedResponse<T> CreatePagedResponse<T>(List<T> items, int pageNumber, int pageSize)
    {
        var totalItems = items.Count;
        var paginatedItems = items.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        return new PagedResponse<T>
        {
            TotalItems = totalItems,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = totalPages,
            HasNext = pageNumber < totalPages,
            HasPrevious = pageNumber > 1,
            Items = paginatedItems
        };
    }
}

