using WebApp.Application.DtoResponse.ReviewsDtoResponse;
using WebApp.Domain.Entity;

namespace WebApp.Application.Mappers.Interfaces;

public interface IReviewMapper
{
    IEnumerable<DtoGetReviewResponse> MapToReviewResponses(IEnumerable<Review> reviews);
}