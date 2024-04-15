namespace WebApp.Domain.Entity;

public class Author
{
    public int Id { get; set; }
    public string Alias { get; set; }
    /// <summary>
    /// Написанные автором книги
    /// </summary>
    public IEnumerable<Book> Books { get; set; }
}