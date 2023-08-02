using System;
using System.Collections.Generic;

namespace PublisherData;

public partial class Author
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual List<Book> Books { get; set; } = new List<Book>();
}
