using OnlineClassbook.Entities;

namespace OnlineClassbook.Data;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Address> Addresses => Set<Address>();
}