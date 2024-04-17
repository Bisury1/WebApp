using WebApp.Application.DtoRequest.ReviewDtoRequest;
using WebApp.Application.DtoResponse.ReviewsDtoResponse;

namespace WebApp.Application.ApiService.ReviewService;

public interface IReviewService
{
    Task<DtoGetReviewResponse> GetReview(int id);
    Task<int> CreateReview(DtoCreateReviewRequest createReviewRequest);
    Task<bool> DeleteReview(DtoDeleteReviewRequest deleteReviewRequest);
}