namespace WebApp.Models;

public class UpdateBookRequest
{
    public string Title { get; set; }
    public IEnumerable<int> AuthorsIds { get; set; }
    public IEnumerable<int> ReviewsIds { get; set; }
}