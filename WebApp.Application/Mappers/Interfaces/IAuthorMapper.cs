using WebApp.Application.DtoResponse.AuthorDtoResponse;
using WebApp.Domain.Entity;

namespace WebApp.Application.Mappers.Interfaces;

public interface IAuthorMapper
{
    DtoGetAuthorResponse MapToGetAuthorResponse(Author author);
    IEnumerable<DtoGetAuthorResponse> MapToGetAuthorResponses(IEnumerable<Author> author);
}