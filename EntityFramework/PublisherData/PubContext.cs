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
            new Author { Id = 2, FirstName = "Sofia", LastName = "Segovia" },
            new Author { Id = 3, FirstName = "Hasan", LastName = "Piker" },
            new Author { Id = 4, FirstName = "Joe", LastName = "Abercrombie" },
            new Author { Id = 5, FirstName = "Stephen", LastName = "King" });

        modelBuilder.Entity<Book>().HasData(
            new Book { BookId = 1, Title = "Before they are hanged", PublishDate =  DateTime.Now, BasePrice = 20.0m,
            AuthorId = 4}
            );   
    }
} 