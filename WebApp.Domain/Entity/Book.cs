namespace WebApp.Domain.Entity;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public IEnumerable<Author> Authors { get; set; }
    public IEnumerable<Review> Reviews { get; set; }
}