using WebApp.Application.DtoRequest.AuthorDtoRequest;
using WebApp.Application.DtoResponse.AuthorDtoResponse;
using WebApp.Application.Mappers.Interfaces;
using WebApp.Domain.Entity;

namespace WebApp.Application.Mappers;

public class AuthorMapper : IAuthorMapper
{
    public DtoGetAuthorResponse MapToGetAuthorResponse(Author author)
        => new()
        {
            Id = author.Id,
            Alias = author.Alias
        };

    public IEnumerable<DtoGetAuthorResponse> MapToGetAuthorResponses(IEnumerable<Author> author)
        => author.Select(MapToGetAuthorResponse);
    
    public Author MapToAuthor(DtoCreateAuthorRequest author)
        => new()
        {
            Alias = author.Alias
        };
}