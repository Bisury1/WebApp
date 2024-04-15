using WebApp.Application.Common;

namespace WebApp.Application.DtoRequest.BookDtoRequest;

public class DtoCreateBookRequest
{
    private DtoCreateBookRequest(string title, IEnumerable<int> authorsIds)
    {
        Title = title;
        AuthorsIds = authorsIds;
    }

    public string Title { get; }
    public IEnumerable<int> AuthorsIds { get; }

    public static Result<DtoCreateBookRequest, string> Create(string title, IEnumerable<int> authorsIds)
    {
        if (string.IsNullOrEmpty(title))
            return "Введено неверное название книги";

        if (!authorsIds.Any())
            return "Не введены авторы";

        return new DtoCreateBookRequest(title, authorsIds);
    }
}