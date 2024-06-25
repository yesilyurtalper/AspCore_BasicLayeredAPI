using BasicLayeredService.API.Contracts.Persistence;
using BasicLayeredService.API.DBContext;
using BasicLayeredService.API.Domain;
using BasicLayeredService.API.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BasicLayeredService.API.Services.Persistence;

public class DbEventRepo : DbBaseItemRepo<Event>, IEventRepo
{
    public DbEventRepo(BaseItemDbContext context) : base(context)
    {

    }

    public async Task<QueryResult<Event>> QueryAsync(QueryDto dto)
    {
        var query = _dbContext.Events.AsQueryable().AsNoTracking();

        if (dto.Id != null && dto.Id != 0)
            query = query.Where(e => e.Id == dto.Id);

        if (!string.IsNullOrEmpty(dto.Author))
            query = query.Where(e => e.Author.Contains(dto.Author));

        if (!string.IsNullOrEmpty(dto.Title))
            query = query.Where(e => !string.IsNullOrEmpty(e.Title) && e.Title.Contains(dto.Title));

        if (!string.IsNullOrEmpty(dto.Body))
            query = query.Where(e => e.Body.Contains(dto.Body));

        if (dto.DateStart != null)
            query = query.Where(e => e.Date >= dto.DateStart);

        if (dto.DateEnd != null)
            query = query.Where(e => e.Date <= dto.DateEnd);

        if (dto.PriceStart != null && dto.PriceStart != 0)
            query = query.Where(e => e.Price >= dto.PriceStart);

        if (dto.PriceEnd != null && dto.PriceEnd != 0)
            query = query.Where(e => e.Price <= dto.PriceEnd);

        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)dto.PageSize);

        var items = new List<Event>();

        if (dto.Descending)
            query = query.OrderByDescending(item => item.Date);
        else
            query = query.OrderBy(item => item.Date);

        items = await query.Skip((dto.PageNumber - 1) * dto.PageSize)
                                     .Take(dto.PageSize)
                                     .ToListAsync();

        return new QueryResult<Event>(items, totalItems, totalPages);
    }
}
