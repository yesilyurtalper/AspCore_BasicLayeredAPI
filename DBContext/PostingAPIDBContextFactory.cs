using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using BasicLayeredService.API.DBContext;

namespace BasicLayeredService.API.DBContext;
public class PostingAPIDBContextFActory : IDesignTimeDbContextFactory<PostingAPIDbContext>
{
    public PostingAPIDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PostingAPIDbContext>();
        optionsBuilder.UseMySQL("server=localhost;port=3306;database=BasicLayeredService;user=root;password=pass;");

        return new PostingAPIDbContext(optionsBuilder.Options);
    }
}