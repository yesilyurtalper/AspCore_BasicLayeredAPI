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

    public async Task<List<Event>> QueryAsync(QueryDto dto)
    {
        //await Task.Delay(3000);
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

        int count = dto.Count ?? 100;
        if(count > 1000)
            count = 1000;

        int direction = dto.Direction ?? 0;

        if(dto.Direction == 1)
            return await query
                .OrderByDescending(e => e.Id)
                .Where(e => e.Id < dto.LastId)
                .Take(count)
                .ToListAsync();
        else if(dto.Direction == -1)
            return await query
                .OrderBy(e => e.Id)
                .Where(e => e.Id > dto.LastId)
                .Take(count)
                .ToListAsync();
        else
            return await query
                .OrderByDescending(e => e.Id)
                .Take(count)
                .ToListAsync();
    }
}
