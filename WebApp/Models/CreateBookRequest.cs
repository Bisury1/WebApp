namespace WebApp.Models;

public class CreateBookRequest
{
    /// <summary>
    /// Название книги
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// Идентификаторы авторов
    /// </summary>
    public IEnumerable<int> AuthorsIds { get; set; }
}