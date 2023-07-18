using PublisherData;
using PublisherDomain;

using (PubContext context = new PubContext())
{
    context.Database.EnsureCreated();
}

void AddAuthor()
{
    var author = new Author { FirstName = "Patrick", LastName = "Rothfuss" };

    using var context = new PubContext();

    context.Authors.Add(author);

    context.SaveChanges();
}

GetAuthors();

AddAuthor();

GetAuthors();

void GetAuthors()
{
    using var context  = new PubContext();
    var authors = context.Authors.ToList();

    authors.ForEach(x => Console.Write(x.FirstName + " " + x.LastName));
}
