using OnlineClassbook.Entities;

namespace OnlineClassbook.Data;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        // EnsureCreated();
    }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Student>()
    //         .HasMany(c => c.Courses)
    //         .WithMany(s => s.Students);
    // }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Mark> Marks => Set<Mark>();
    public DbSet<Course> Courses => Set<Course>();
    
//     public bool EnsureCreated() => Database.EnsureCreated();
//     public bool EnsureDeleted() => Database.EnsureDeleted();
//     public bool EnsureReset() => EnsureDeleted() && EnsureCreated();
}