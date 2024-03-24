using Microsoft.AspNetCore.Identity;

namespace WebApp.Persistence.Model;

public class DbReview
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Content { get; set; }
    public byte Grade { get; set; }
    /// <summary>
    /// Книга, которую оценивают
    /// </summary>
    public DbBook Book { get; set; }
    /// <summary>
    /// Пользователь, написавший отзыв
    /// </summary>
    public DbUser User { get; set; }
}