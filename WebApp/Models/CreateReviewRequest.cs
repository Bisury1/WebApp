namespace WebApp.Models;

public class CreateReviewRequest
{
    public string Title { get; init; }
    public string? Content { get; set; }
    public byte Grade { get; init; }
    public int BookId { get; init; }
}