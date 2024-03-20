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
            "Data Source = localhost; Initial Catalog = Publisher; Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
            ).LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name } );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
/*        modelBuilder.Entity<Author>().HasData(
            new Author { Id = 1, FirstName = "Rhoda", LastName = "Lerman" },
            new Author { Id = 2, FirstName = "Sofia", LastName = "Segovia"});*/
            modelBuilder.Entity<Author>()
                .HasMany( a => a.Books )
                .WithOne( b => b.Author )
                .HasForeignKey(b  => b.AuthorId)
                .IsRequired(false);

           modelBuilder.Entity<Author>().HasData(
            new Author { Id = 2, FirstName = "Sofia", LastName = "Segovia" },
            new Author { Id = 3, FirstName = "Hasan", LastName = "Piker" },
            new Author { Id = 4, FirstName = "Joe", LastName = "Abercrombie" },
            new Author { Id = 5, FirstName = "Stephen", LastName = "King" });

        modelBuilder.Entity<Author>()
    .HasMany(a => a.Books)
    .WithOne(b => b.Author)
    .HasForeignKey(b => b.AuthorId)
    .IsRequired(false);

        modelBuilder.Entity<Book>().HasData(
            new Book { Title = "Entity Framework", BookId = 1, AuthorId = 2, BasePrice = 8.0m, Genre = "Coding", PublishDate = DateTime.Now }
            );

        modelBuilder.Entity<Book>().HasData(
            new Book { BookId = 2, Title = "Before they are hanged", PublishDate =  DateTime.Now, BasePrice = 20.0m,
            AuthorId = 4}
            );   


    }
} 