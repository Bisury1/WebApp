using WebApp.Application.Common;

namespace WebApp.Application.DtoRequest.AuthorDtoRequest;

public class DtoDeleteAuthorRequest
{
    private DtoDeleteAuthorRequest(int id)
    {
        Id = id;
    }
    public int Id { get; init; }
    
    public static Result<DtoDeleteAuthorRequest, string> Create(int id)
    {
        if (id < 0)
            return "Введен неверный идентификатор автора";

        return new DtoDeleteAuthorRequest(id);
    }
}