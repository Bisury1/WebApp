using WebApp.Application.DtoRequest.ReviewDtoRequest;
using WebApp.Application.DtoResponse.ReviewsDtoResponse;
using WebApp.Domain.Entity;

namespace WebApp.Application.Mappers.Interfaces;

public interface IReviewMapper
{
    IEnumerable<DtoGetReviewResponse> MapToReviewResponses(IEnumerable<Review> reviews);
    Review MapToReview(DtoCreateReviewRequest review, User user, Book book);
    DtoGetReviewResponse MapToReviewResponse(Review review);
}