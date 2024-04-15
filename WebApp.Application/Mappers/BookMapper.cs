

using WebApp.Application.DtoRequest.BookDtoRequest;
using WebApp.Application.DtoResponse.BookDtoResponse;
using WebApp.Application.Mappers.Interfaces;
using WebApp.Domain.Entity;

namespace WebApp.Application.Mappers;

public class BookMapper(IAuthorMapper authorMapper, IReviewMapper reviewMapper): IBookMapper
{
    public DtoGetBookResponse MapToGetBookResponse(Book book)
        => new()
        {
            Id = book.Id,
            Title = book.Title,
            Authors = authorMapper.MapToGetAuthorResponses(book.Authors),
            Reviews = reviewMapper.MapToReviewResponses(book.Reviews)
        };
    
    private DtoGetBookLightResponse MapToGetBookLightResponse(Book book)
        => new()
        {
            Id = book.Id,
            Title = book.Title,
            Authors = authorMapper.MapToGetAuthorResponses(book.Authors)
        };

    public IEnumerable<DtoGetBookLightResponse> MapToGetBookResponses(IEnumerable<Book> book)
        => book.Select(MapToGetBookLightResponse);
}