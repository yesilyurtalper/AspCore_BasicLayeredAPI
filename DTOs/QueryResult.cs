namespace BasicLayeredService.API.DTOs;

public class QueryResult<T>  where T : class
{
    public T Items { get; set; }
    public int Count { get; set; }


    public QueryResult(T items, int count) 
    {
        Items = items;
        Count = count;
    }
}