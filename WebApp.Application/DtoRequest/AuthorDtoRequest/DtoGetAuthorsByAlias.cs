using WebApp.Application.Common;

namespace WebApp.Application.DtoRequest.AuthorDtoRequest;

public class DtoGetAuthorsByAlias
{
    public string Alias { get; init; }

    private DtoGetAuthorsByAlias(string alias)
    {
        Alias = alias;
    }
    
    public static Result<DtoGetAuthorsByAlias, string> Create(string alias)
    {
        if (string.IsNullOrEmpty(alias))
            return "Не введен псевдоним автора";

        return new DtoGetAuthorsByAlias(alias);
    }
}