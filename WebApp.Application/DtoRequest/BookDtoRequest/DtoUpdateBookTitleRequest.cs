using WebApp.Application.Common;

namespace WebApp.Application.DtoRequest.BookDtoRequest;

public class DtoUpdateBookTitleRequest
{
    private DtoUpdateBookTitleRequest(int id, string title)
    {
        Id = id;
        Title = title;
    }
    
    public int Id { get; }
    public string Title { get; }
    
    public static Result<DtoUpdateBookTitleRequest, string> Create(int id, string title)
    {
        if (id < 0)
        {
            return "Введен неверный идентификатор книги";
        }

        if (string.IsNullOrEmpty(title))
            return "Введено неверное название книги";
        
        return new DtoUpdateBookTitleRequest(id, title);
    }
    
}