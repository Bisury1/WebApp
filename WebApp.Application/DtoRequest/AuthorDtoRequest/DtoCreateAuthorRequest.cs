using WebApp.Application.Common;

namespace WebApp.Application.DtoRequest.AuthorDtoRequest;

public class DtoCreateAuthorRequest
{
    public string Alias { get; init; }
    private DtoCreateAuthorRequest(string alias)
    {
        Alias = alias;
    }
    
    public static Result<DtoCreateAuthorRequest, string> Create(string alias)
    {
        if (string.IsNullOrEmpty(alias))
            return "Не введен псевдоним автора";

        return new DtoCreateAuthorRequest(alias);
    }
}