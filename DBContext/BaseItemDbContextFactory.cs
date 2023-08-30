using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using BasicLayeredService.API.DBContext;

namespace BasicLayeredService.API.DBContext;
public class BaseItemDbContextFactory : IDesignTimeDbContextFactory<BaseItemDbContext>
{
    public BaseItemDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BaseItemDbContext>();
        optionsBuilder.UseMySQL("server=localhost;port=3306;database=BasicLayeredService;user=root;password=pass;");

        return new BaseItemDbContext(optionsBuilder.Options);
    }
}