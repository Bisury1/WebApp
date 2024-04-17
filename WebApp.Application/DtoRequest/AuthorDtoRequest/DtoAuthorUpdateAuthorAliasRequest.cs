using WebApp.Application.Common;

namespace WebApp.Application.DtoRequest.AuthorDtoRequest;

public class DtoAuthorUpdateAuthorAliasRequest
{
    public int Id { get; init; }
    public string Alias { get; init; }

    private DtoAuthorUpdateAuthorAliasRequest(int id, string alias)
    {
        Id = id;
        Alias = alias;
    }
    public static Result<DtoAuthorUpdateAuthorAliasRequest, string> Create(int id, string alias)
    {
        if (id < 0)
            return "Введен неверный идентификатор автора";

        if (string.IsNullOrEmpty(alias))
            return "Не введен псевдоним автора";

        return new DtoAuthorUpdateAuthorAliasRequest(id, alias);
    }
}