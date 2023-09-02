using BasicLayeredService.API.Contracts.Persistence;
using BasicLayeredService.API.DBContext;
using BasicLayeredService.API.Domain;
using BasicLayeredService.API.DTOs;

namespace BasicLayeredService.API.Services.Persistence;

public class DbEventRepo : DbBaseItemRepo<Event>, IEventRepo    
{
    public DbEventRepo(BaseItemDbContext context) : base(context)
    {
        
    }

    public async Task<List<Event>> QueryAsync(QueryDto dto)
    {
        await Task.Delay(3000);
        var query = _dbContext.Events.AsQueryable();

        if (dto.Id != null)
            query = query.Where(e => e.Id == dto.Id);

        return query.ToList();
    }
}
