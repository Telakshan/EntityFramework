namespace PublisherDomain;

public class Author
{
    public Author() 
    { 
        Books = new List<Book>();
    }

    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public List<Book> Books { get; set; }
}