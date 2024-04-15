
using WebApp.Application.DtoResponse.ReviewsDtoResponse;
using WebApp.Application.Mappers.Interfaces;
using WebApp.Domain.Entity;

namespace WebApp.Application.Mappers;

public class ReviewMapper(IUserMapper userMapper) : IReviewMapper
{
    public IEnumerable<DtoGetReviewResponse> MapToReviewResponses(IEnumerable<Review> reviews)
        => reviews.Select(MapToReviewResponse);

    private DtoGetReviewResponse MapToReviewResponse(Review review)
        => new()
        {
            Id = review.Id,
            Title = review.Title,
            Content = review.Content,
            Grade = review.Grade,
            Users = userMapper.MapToUserResponse(review.User)
        };
}