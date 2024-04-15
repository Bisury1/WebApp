namespace WebApp.Domain.Entity;

public class Review
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Content { get; set; }
    public byte Grade { get; set; }
    /// <summary>
    /// Книга, которую оценивают
    /// </summary>
    public Book Book { get; set; }
    /// <summary>
    /// Пользователь, написавший отзыв
    /// </summary>
    public User User { get; set; }
}