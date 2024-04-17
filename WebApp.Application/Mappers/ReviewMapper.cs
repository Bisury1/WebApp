
using WebApp.Application.DtoRequest.ReviewDtoRequest;
using WebApp.Application.DtoResponse.ReviewsDtoResponse;
using WebApp.Application.Mappers.Interfaces;
using WebApp.Domain.Entity;

namespace WebApp.Application.Mappers;

public class ReviewMapper(IUserMapper userMapper) : IReviewMapper
{
    public IEnumerable<DtoGetReviewResponse> MapToReviewResponses(IEnumerable<Review> reviews)
        => reviews.Select(MapToReviewResponse);

    public DtoGetReviewResponse MapToReviewResponse(Review review)
        => new()
        {
            Id = review.Id,
            Title = review.Title,
            Content = review.Content,
            Grade = review.Grade,
            User = userMapper.MapToUserResponse(review.User)
        };
    
    public Review MapToReview(DtoCreateReviewRequest review, User user, Book book)
        => new()
        {
            Title = review.Title,
            Content = review.Content,
            Grade = review.Grade,
            User = user,
            Book = book
        };
}