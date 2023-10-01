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

        if (dto.Id != null && dto.Id != 0)
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

        if (dto.PriceStart != null && dto.PriceStart != 0)
            query = query.Where(e => e.Price >= dto.PriceStart);

        if (dto.PriceEnd != null && dto.PriceEnd != 0)
            query = query.Where(e => e.Price <= dto.PriceEnd);

        var count = await query.CountAsync();

        var items = new List<Event>();

        if(dto.LastId != null)
        {
            query = query.OrderByDescending(e => e.Id);
            if(dto.LastId <= 0)
                items = await query.Take(dto.PageSize).ToListAsync();
            else
                items = await query.Where(e => e.Id < dto.LastId).Take(dto.PageSize).ToListAsync();
        }
            
        else if(dto.FirstId != null)
        {
            query = query.OrderBy(e => e.Id);
            items = await query.Where(e => e.Id >  dto.FirstId).Take(dto.PageSize).ToListAsync();
            items.Reverse();
        }

        return new QueryResult<List<Event>>(items,count);
    }
}
