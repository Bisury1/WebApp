using WebApp.Application.DtoRequest.AuthorDtoRequest;
using WebApp.Application.DtoResponse.AuthorDtoResponse;

namespace WebApp.Application.ApiService.AuthorService;

public interface IAuthorService
{
    Task<DtoGetAuthorsResponse> GetAuthors();
    Task<DtoGetAuthorsResponse> GetAuthors(DtoGetAuthorsByAlias getAuthorsByAliasRequest);
    Task<DtoGetAuthorResponse> GetAuthor(int id);
    Task<int> CreateAuthor(DtoCreateAuthorRequest createAuthorRequest);
    Task<bool> ChangeAuthorAlias(DtoAuthorUpdateAuthorAliasRequest dtoAuthorAliasRequest);
    Task<bool> DeleteAuthor(DtoDeleteAuthorRequest deleteAuthorRequest);
}