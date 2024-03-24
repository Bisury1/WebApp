namespace WebApp.Persistence.Model;

public class DbBook
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public IEnumerable<DbAuthor> Authors { get; set; }
    public IEnumerable<DbReview>? Reviews { get; set; }
}