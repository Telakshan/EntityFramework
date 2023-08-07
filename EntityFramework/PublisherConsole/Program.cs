using Microsoft.EntityFrameworkCore;
using PublisherData;

/*using (PubContext context = new PubContext())
{
    context.Database.EnsureCreated();
}*/

PubContext _context = new PubContext();

List<Author> _authors = new List<Author>() { 
        new Author { FirstName = "Patrick", LastName = "Rothfuss" },
        new Author { FirstName = "Brandon", LastName = "Sanderson" },
        new Author { FirstName = "H.P.", LastName = "Lovecraft" },

                                            };

List<Author> _authors2 = new List<Author>()
{
        new Author { FirstName = "Stephen", LastName = "King" },
        new Author { FirstName = "Joe", LastName = "Abercrombie" },
        new Author { FirstName = "Patrick", LastName = "King" }
};

//Method calls
//GetAuthors();

//AddAuthor();

GetAuthors();

//AddAuthor();

//AddMultipleAuthors();

//AddMultipleAuthorsList(_authors);

//GetAuthors();

//SkipAndTakeAuthors();

//FindIt();

//QueryFilters();

//SortAuthors();

//QueryAggregate();

//UpdateAuthors();

//DropTables();

//Truncate();

void AddAuthor()
{
    var author = new Author { FirstName = "Patrick", LastName = "Rothfuss" };
    var author2 = new Author { FirstName = "Brandon", LastName = "Sanderson" };

    _context.Authors.Add(author);
    _context.Authors.Add(author2);
    _context.Authors.Add(new Author { FirstName = "H.P.", LastName = "Lovecraft" });
    _context.Authors.Add(new Author { FirstName = "Stephen", LastName = "King" });
    _context.Authors.Add(new Author { FirstName = "Joe", LastName = "Abercrombie" });
    _context.Authors.Add(new Author { FirstName = "George RR.", LastName = "Martin" });
    _context.Authors.Add(new Author { FirstName = "Joseph", LastName = "King" });

    _context.SaveChanges();
} 

void AddMultipleAuthors()
{
    _context.Authors.AddRange(new Author { FirstName = "Patrick", LastName = "Rothfuss" },
        new Author { FirstName = "Brandon", LastName = "Sanderson" },
        new Author { FirstName = "H.P.", LastName = "Lovecraft" },
        new Author { FirstName = "Stephen", LastName = "King" },
        new Author { FirstName = "Joe", LastName = "Abercrombie" },
        new Author { FirstName = "George RR.", LastName = "Martin" },
        new Author { FirstName = "Joseph", LastName = "King" });

    _context.SaveChanges();
}
void AddMultipleAuthorsList(IList<Author> authors)
{
    _context.Authors.AddRange(_authors);
    _context.SaveChanges();
}

void AddAuthorWithBooks()
{
    var author = new Author { FirstName = "Patrick", LastName = "Rothfuss" };

    author.Books.Add(new Book { Title = "Name of the wind", PublishDate = new DateTime(2012, 1, 1) }); 
    author.Books.Add(new Book { Title = "Wise man's fear", PublishDate = new DateTime(2013, 1, 1) });

    _context.Authors.Add(author);
    _context.SaveChanges();
}

void UpdateAuthors()
{

    var hp = _context.Authors.FirstOrDefault(a => a.LastName == "Lovecraft");

    if (hp == null) throw new Exception("TRASH!");

    hp.FirstName = "Harry Potter";

    Console.WriteLine("Before: " + _context.ChangeTracker.DebugView.ShortView);

    var hpAfterUpdate = _context.Authors.FirstOrDefault(a => a.LastName == "Lovecraft");

    Console.WriteLine($"hpAfterUpdate: {hpAfterUpdate?.FirstName} {hpAfterUpdate?.LastName}\n");

    _context.ChangeTracker.DetectChanges();

    Console.WriteLine($"After: {_context.ChangeTracker.DebugView.ShortView}");

    _context.SaveChanges();

}

void Coordinate()
{
    var author = FindThatAuthor(3);

    if (author?.LastName == "Lovecraft")
    {
        author.FirstName = "Harry Potter";
        SaveThatAuthor(author);
    }
}

void SaveThatAuthor(Author author)
{
    using var anothershortLivedContext = new PubContext();
    anothershortLivedContext.Authors.Update(author);
    anothershortLivedContext.SaveChanges();
}

Author FindThatAuthor(int authorId)
{
    using var shortLivedContext = new PubContext();

    Console.WriteLine("Before: " + shortLivedContext.ChangeTracker.DebugView.ShortView);

    shortLivedContext.ChangeTracker.DetectChanges();

    Console.WriteLine($"After: {shortLivedContext.ChangeTracker.DebugView.ShortView}");

    return shortLivedContext.Authors?.Find(authorId);
}

void SortAuthors()
{
    var authorsByLastName = _context.Authors.OrderBy(a => a.LastName).ThenBy(a => a.FirstName).ToList();
    
    authorsByLastName.ForEach(a => Console.WriteLine($"{a.LastName}, {a.FirstName}"));
}

void GetAuthors()
{
    var authors = _context.Authors.Include(x => x.Books).ToList();

    if (authors.Count == 0) Console.WriteLine($"Authors count: {authors.Count}");

    authors.ForEach(x => Console.WriteLine($"{x.FirstName} {x.LastName}"));

    var authorWithBooks = authors.FirstOrDefault(x => x.Books.Count > 0);

    if (authorWithBooks != null)
    {
        authorWithBooks.Books.ForEach(x => Console.WriteLine($"\nBooks\n * {x.Title} - {x.Author.FirstName} {x.Author.LastName}"));
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
    _context.Database.ExecuteSqlRaw("DROP TABLE Books");
    _context.Database.ExecuteSqlRaw("DROP TABLE Authors");
}

void Truncate()
{
    _context.Books.ExecuteDelete();
    _context.Authors.ExecuteDelete();
    _context.SaveChanges();
}

void LinqSelectTest()
{
    var selected =_authors.Select((x, index) => _authors[index].FirstName = _authors2[index].FirstName);

    _authors.ForEach(x => Console.WriteLine($"\n{x.FirstName} {x.LastName}"));
}

LinqSelectTest();
