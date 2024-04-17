using WebApp.Application.DtoResponse.DtoUserResponse;

namespace WebApp.Application.DtoResponse.ReviewsDtoResponse;

public class DtoGetReviewResponse
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public string? Content { get; init; }
    public byte Grade { get; init; }
    public required DtoGetUserResponse User { get; init; }
}