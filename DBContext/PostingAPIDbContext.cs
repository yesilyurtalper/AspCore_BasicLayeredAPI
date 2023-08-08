using Microsoft.EntityFrameworkCore;
using BasicLayeredAPI.API.Domain;

namespace BasicLayeredAPI.API.DBContext;

public class PostingAPIDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PostingAPIDbContext(DbContextOptions<PostingAPIDbContext> options,
        IHttpContextAccessor accessor) : base(options)
    {
        _httpContextAccessor = accessor;
    }

    public DbSet<Post> Posts { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<Post>()
            .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
        {     
            if (entry.State == EntityState.Added)
            {
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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostingAPIDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
