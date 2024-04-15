using WebApp.Application.DtoResponse.AuthorDtoResponse;
using WebApp.Application.DtoResponse.ReviewsDtoResponse;

namespace WebApp.Application.DtoResponse.BookDtoResponse;

public class DtoGetBookResponse
{
    public int Id { get; init; }
    public string? Title { get; init; }
    public required IEnumerable<DtoGetAuthorResponse> Authors { get; init; }
    public required IEnumerable<DtoGetReviewResponse> Reviews { get; init; }
}