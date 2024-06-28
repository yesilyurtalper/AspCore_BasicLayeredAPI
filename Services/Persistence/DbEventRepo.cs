using BasicLayeredService.API.Contracts.Persistence;
using BasicLayeredService.API.DBContext;
using BasicLayeredService.API.Domain;
using BasicLayeredService.API.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BasicLayeredService.API.Services.Persistence;

public class DbEventRepo : DbBaseItemRepo<Event,EventQueryDto>
{
    public DbEventRepo(BaseItemDbContext context) : base(context)
    {

    }

    public async override Task<QueryResult<Event>> QueryAsync(EventQueryDto dto)
    {
        var query = _dbSet.AsQueryable().AsNoTracking();

        if (dto.Id != null)
            query = query.Where(e => e.Id == dto.Id);

        if (!string.IsNullOrEmpty(dto.Author))
            query = query.Where(e => e.Author == dto.Author);

        if (!string.IsNullOrEmpty(dto.Title))
            query = query.Where(e => !string.IsNullOrEmpty(e.Title) && e.Title.Contains(dto.Title));

        if (!string.IsNullOrEmpty(dto.Body))
            query = query.Where(e => e.Body.Contains(dto.Body));

        if (dto.DateCreatedStart != null)
            query = query.Where(e => e.DateCreated >= dto.DateCreatedStart);

        if (dto.DateCreatedEnd != null)
            query = query.Where(e => e.DateCreated <= dto.DateCreatedEnd);

        if (dto.DateModifiedStart != null)
            query = query.Where(e => e.DateModified >= dto.DateModifiedStart);

        if (dto.DateModifiedEnd != null)
            query = query.Where(e => e.DateModified <= dto.DateModifiedEnd);

        if (dto.DateStart != null)
            query = query.Where(e => e.Date >= dto.DateStart);

        if (dto.DateEnd != null)
            query = query.Where(e => e.Date <= dto.DateEnd);

        if (dto.PriceStart != null)
            query = query.Where(e => e.Price >= dto.PriceStart);

        if (dto.PriceEnd != null)
            query = query.Where(e => e.Price <= dto.PriceEnd);

        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)dto.PageSize);

        var items = new List<Event>();

        if (dto.SortByAuthor == 1)
            query = query.OrderBy(item => item.Author);
        else if (dto.SortByAuthor == -1)
            query = query.OrderByDescending(item => item.Author);

        if (dto.SortByTitle == 1)
            query = query.OrderBy(item => item.Title);
        else if (dto.SortByTitle == -1)
            query = query.OrderByDescending(item => item.Title);

        if (dto.SortByBody == 1)
            query = query.OrderBy(item => item.Body);
        else if (dto.SortByBody == -1)
            query = query.OrderByDescending(item => item.Body);

        if (dto.SortByPrice == 1)
            query = query.OrderBy(item => item.Price);
        else if (dto.SortByPrice == -1)
            query = query.OrderByDescending(item => item.Price);

        if (dto.SortByDate == 1)
            query = query.OrderBy(item => item.Date);
        else if (dto.SortByDate == -1)
            query = query.OrderByDescending(item => item.Date);

        if (dto.SortByDateCreated == 1)
            query = query.OrderBy(item => item.DateCreated);
        else if (dto.SortByDateCreated == -1)
            query = query.OrderByDescending(item => item.DateCreated);

        if (dto.SortByDateModified == 1)
            query = query.OrderBy(item => item.DateModified);
        else if (dto.SortByDateModified == -1)
            query = query.OrderByDescending(item => item.DateModified);

        items = await query.Skip((dto.PageNumber - 1) * dto.PageSize)
                                     .Take(dto.PageSize)
                                     .ToListAsync();

        return new QueryResult<Event>(items, totalItems, totalPages);
    }
}
