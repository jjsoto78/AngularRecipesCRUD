
namespace Persistence;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Recipe> Recipes {get; set;} // abstraction for Recipes Table
}
