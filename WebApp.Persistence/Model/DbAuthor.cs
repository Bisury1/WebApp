namespace WebApp.Persistence.Model;

public class DbAuthor
{
    public int Id { get; set; }
    public string Alias { get; set; }
    /// <summary>
    /// Написанные автором книги
    /// </summary>
    public IEnumerable<DbBook> Books { get; set; }
}