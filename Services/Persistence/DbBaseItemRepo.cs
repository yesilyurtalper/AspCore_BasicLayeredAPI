
using BasicLayeredService.API.Domain;
using BasicLayeredService.API.DBContext;
using BasicLayeredService.API.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BasicLayeredService.API.Services.Persistence;

public class DbBaseItemRepo<TModel> : IBaseItemRepo<TModel> where TModel : BaseItem
{
    protected readonly BaseItemDbContext _dbContext;
    protected DbSet<TModel> _dbSet = null;

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

    public virtual async Task<TModel> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id); 
    }

    public virtual async Task<List<TModel>> GetByAuthorAsync(string author)
    {
        return await _dbSet.Where(x => x.Author == author).ToListAsync();
    }

    public virtual async Task<List<TModel>> GetLatestAsync(int count)
    {
        return await _dbSet.OrderByDescending(p => p.DateModified).
            Take(count).ToListAsync();
    }
}
