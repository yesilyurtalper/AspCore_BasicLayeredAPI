namespace BasicLayeredService.API.DTOs;

public class QueryResult<T>  where T : class
{
    public List<T> Items { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }


    public QueryResult(List<T> items, int totalItems, int totalPages) 
    {
        Items = items;
        TotalItems = totalItems;
        TotalPages = totalPages;
    }
}