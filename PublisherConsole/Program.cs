using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;
using System.Net.WebSockets;

using (PubContext context = new PubContext())
{
    context.Database.EnsureCreated();
}

PubContext _context = new PubContext();

void AddAuthor()
{
    var author = new Author { FirstName = "Patrick", LastName = "Rothfuss" };
    var author2 = new Author { FirstName = "Brandon", LastName = "Sanderson" };

    using var context = new PubContext();

    context.Authors.Add(author);
    context.Authors.Add(author2);
    context.Authors.Add(new Author { FirstName = "H.P.", LastName = "Lovecraft" });
    context.Authors.Add(new Author { FirstName = "Stephen", LastName = "King" });
    context.Authors.Add(new Author { FirstName = "Joe", LastName = "Abercrombie" });
    context.Authors.Add(new Author { FirstName = "George RR.", LastName = "Martin" });
    context.Authors.Add(new Author { FirstName = "Joseph", LastName = "King" });

    context.SaveChanges();
} 

void AddAuthorWithBooks()
{
    var author = new Author { FirstName = "Patrick", LastName = "Rothfuss" };

    author.Books.Add(new Book { Title = "Name of the wind", PublishDate = new DateTime(2012, 1, 1) });
    author.Books.Add(new Book { Title = "Wise man's fear", PublishDate = new DateTime(2013, 1, 1) });

    using var context = new PubContext();   

    context.Authors.Add(author);
    context.SaveChanges();
}

/*GetAuthors();

AddAuthor();

GetAuthors();*/

AddAuthor();

/*GetAuthors();*/

//SkipAndTakeAuthors();

//FindIt();

//QueryFilters();

//SortAuthors();

QueryAggregate();

DropTables();


void SortAuthors()
{
    var authorsByLastName = _context.Authors.OrderBy(a => a.LastName).ThenBy(a => a.FirstName).ToList();
    
    authorsByLastName.ForEach(a => Console.WriteLine($"{a.LastName}, {a.FirstName}"));
}

void GetAuthors()
{
    using var context  = new PubContext();
    var authors = context.Authors.Include(x => x.Books).ToList();

    authors.ForEach(x => Console.WriteLine($"{x.FirstName} {x.LastName}"));

    var patrick = authors.FirstOrDefault(x => x.FirstName == "Patrick");

    if (patrick != null)
    {
        patrick.Books.ForEach(x => Console.WriteLine($" * {x.Title}"));
    }

}

void QueryAggregate()
{
    var author = _context.Authors.FirstOrDefault(a => a.FirstName == "Stephen");

    Console.WriteLine($"QueryAggregate: {author?.FirstName} {author?.LastName}");
}

void SkipAndTakeAuthors()
{
    var groupSize = 2;

    for (int i=0; i<5; i++)
    {
        var authors = _context.Authors.Skip(groupSize * i).Take(groupSize).ToList();
        Console.WriteLine($"Group {i}:");
        foreach (var author in authors)
        {
            Console.WriteLine($" {author.FirstName} {author.LastName}");
        }
    }
}

void QueryFilters()
{
    var authors = _context.Authors.Where(a => EF.Functions.Like(a.LastName, "R%")).ToList(); //_context.Authors.Where(s => s.FirstName == "Patrick").ToList();

    authors.ForEach(x => Console.WriteLine($"QueryFilters: {x.FirstName} {x.LastName}" ));
}

void FindIt()
{
    var authorIDTwo = _context.Authors.Find(2);

    Console.WriteLine(authorIDTwo?.FirstName);
}

void DropTables()
{
    using var context = new PubContext();

    context.Database.ExecuteSqlRaw("DROP TABLE Books");
    context.Database.ExecuteSqlRaw("DROP TABLE Authors");
}
