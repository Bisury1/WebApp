using WebApp.Application.DtoResponse.AuthorDtoResponse;

namespace WebApp.Application.DtoResponse.BookDtoResponse;

public class DtoGetBookLightResponse
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public required IEnumerable<DtoGetAuthorResponse> Authors { get; init; }
}