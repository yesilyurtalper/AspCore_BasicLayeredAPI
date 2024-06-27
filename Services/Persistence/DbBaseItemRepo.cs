
using BasicLayeredService.API.Domain;
using BasicLayeredService.API.DBContext;
using BasicLayeredService.API.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using BasicLayeredService.API.DTOs;

namespace BasicLayeredService.API.Services.Persistence;

public class DbBaseItemRepo<TModel,TQuery> : IBaseItemRepo<TModel,TQuery> 
    where TModel : BaseItem where TQuery : BaseQueryDto
{
    protected readonly BaseItemDbContext _dbContext;
    protected DbSet<TModel> _dbSet;

    public DbBaseItemRepo(BaseItemDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TModel>();
    }

    public virtual async Task CreateAsync(TModel model)
    {
        await _dbSet.AddAsync(model);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(TModel model)
    {
        _dbSet.Update(model);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(TModel model)
    {
        _dbSet.Remove(model);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task<TModel> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id); 
    }

    public virtual async Task<TModel> GetByIdAsNoTrackingAsync(Guid id)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public virtual async Task<QueryResult<TModel>> QueryAsync(TQuery dto)
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

        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)dto.PageSize);

        var items = new List<TModel>();

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

        return new QueryResult<TModel>(items, totalItems, totalPages);
    }
}
