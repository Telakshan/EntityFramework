using Microsoft.EntityFrameworkCore;
using PublisherDomain;

namespace PublisherData;

public class PubContext: DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Publisher"
            );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>().HasData(
            new Author { Id = 1, FirstName = "Rhoda", LastName = "Lerman" },
            new Author { Id = 2, FirstName = "Sofia", LastName = "Segovia"});
    }
} 