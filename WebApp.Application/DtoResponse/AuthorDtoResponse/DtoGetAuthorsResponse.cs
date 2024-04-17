namespace WebApp.Application.DtoResponse.AuthorDtoResponse;

public class DtoGetAuthorsResponse
{
    public required IEnumerable<DtoGetAuthorResponse> Authors { get; init; }
}