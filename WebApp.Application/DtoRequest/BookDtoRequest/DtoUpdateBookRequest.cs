using WebApp.Application.Common;
using WebApp.Domain.Entity;

namespace WebApp.Application.DtoRequest.BookDtoRequest;

public class DtoUpdateBookRequest
{
    private DtoUpdateBookRequest(int id, string title, IEnumerable<int> authorsIds, IEnumerable<int> reviewsIds)
    {
        Id = id;
        Title = title;
        AuthorsIds = authorsIds;
        ReviewsIds = reviewsIds;
    }

    public int Id { get; }
    public string Title { get; }
    public IEnumerable<int> AuthorsIds { get; }
    public IEnumerable<int> ReviewsIds { get; }

    public static Result<DtoUpdateBookRequest, string> Create(int id, string title, IEnumerable<int> authors,
        IEnumerable<int> reviews)
    {
        if (id < 0)
        {
            return "Введен неверный идентификатор книги";
        }

        if (string.IsNullOrEmpty(title))
            return "Введено неверное название книги";

        if (!authors.Any())
            return "Не введены авторы";
        
        return new DtoUpdateBookRequest(id, title, authors, reviews);
    }
}