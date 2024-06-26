using Microsoft.EntityFrameworkCore;
using BasicLayeredService.API.Domain;
using System.ComponentModel;

namespace BasicLayeredService.API.DBContext;

public class BaseItemDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BaseItemDbContext(DbContextOptions<BaseItemDbContext> options,
        IHttpContextAccessor accessor) : base(options)
    {
        _httpContextAccessor = accessor;
    }

    public BaseItemDbContext(DbContextOptions<BaseItemDbContext> options) : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Event> Events { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseItem>()
            .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
        {     
            if (entry.State == EntityState.Added)
            {
                entry.Entity.Id = Guid.NewGuid(); 
                entry.Entity.Author = _httpContextAccessor.HttpContext.User.Claims.
                    FirstOrDefault(c => c.Type == "preferred_username").Value;
                entry.Entity.DateCreated = DateTime.Now;
                entry.Entity.DateModified = entry.Entity.DateCreated;
            }
            else
                entry.Entity.DateModified = DateTime.Now;
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseItemDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
