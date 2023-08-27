﻿
using BasicLayeredService.API.Domain;
using BasicLayeredService.API.DBContext;
using BasicLayeredService.API.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BasicLayeredService.API.Services.Persistence;

public class DBPostRepo : IPostRepo
{
    protected readonly PostingAPIDbContext _dbContext;
    protected DbSet<Post> _dbSet = null;

    public DBPostRepo(PostingAPIDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<Post>();
    }

    public virtual async Task CreateAsync(Post post)
    {
        await _dbSet.AddAsync(post);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(Post post)
    {
        _dbSet.Update(post);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(Post post)
    {
        _dbSet.Remove(post);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task<Post> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id); 
    }

    public virtual async Task<List<Post>> GetByAuthorAsync(string author)
    {
        return await _dbSet.Where(x => x.Author == author).ToListAsync();
    }

    public virtual async Task<List<Post>> GetLatestAsync(int count)
    {
        return await _dbSet.OrderByDescending(p => p.DateModified).
            Take(count).ToListAsync();
    }
}
