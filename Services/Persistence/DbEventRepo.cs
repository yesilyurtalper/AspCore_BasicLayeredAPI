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

    public async Task<QueryResult<List<Event>>> QueryAsync(QueryDto dto)
    {
        var query = _dbContext.Events.AsQueryable().AsNoTracking();

        if (dto.Id != null)
            query = query.Where(e => e.Id == dto.Id);

        if(!string.IsNullOrEmpty(dto.Author))
            query = query.Where(e => e.Author == dto.Author);

        if (!string.IsNullOrEmpty(dto.Title))
            query = query.Where(e => !string.IsNullOrEmpty(e.Title) && e.Title.Contains(dto.Title));

        if (!string.IsNullOrEmpty(dto.Body))
            query = query.Where(e => e.Body.Contains(dto.Body));

        if (dto.DateStart != null)
            query = query.Where(e => e.Date >= dto.DateStart);

        if (dto.DateEnd != null)
            query = query.Where(e => e.Date <= dto.DateEnd);

        if (dto.PriceStart != null)
            query = query.Where(e => e.Price >= dto.PriceStart);

        if (dto.PriceEnd != null)
            query = query.Where(e => e.Price <= dto.PriceEnd);

        var count = await query.CountAsync();

        query = query.OrderByDescending(e => e.Id);

        if (dto.LastId != null && dto.LastId != 0)
            query = query.Where(e => e.Id < dto.LastId);

        var items = await query.Take(100).ToListAsync();

        return new QueryResult<List<Event>>(items,count);
    }
}
