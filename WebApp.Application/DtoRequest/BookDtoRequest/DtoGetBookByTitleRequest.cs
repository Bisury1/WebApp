using WebApp.Application.Common;

namespace WebApp.Application.DtoRequest.BookDtoRequest;

public class DtoGetBookByTitleRequest
{
    private DtoGetBookByTitleRequest(string title)
    {
        Title = title;
    }

    public string Title { get; }
    
    public static Result<DtoGetBookByTitleRequest, string> Create(string title)
    {
        if (string.IsNullOrEmpty(title))
            return "Введено неверное название книги";

        return new DtoGetBookByTitleRequest(title);
    }
}