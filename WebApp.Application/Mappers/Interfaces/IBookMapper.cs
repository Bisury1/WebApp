using WebApp.Application.DtoResponse.BookDtoResponse;
using WebApp.Domain.Entity;

namespace WebApp.Application.Mappers.Interfaces;

public interface IBookMapper
{
    DtoGetBookResponse MapToGetBookResponse(Book book);
    IEnumerable<DtoGetBookLightResponse> MapToGetBookResponses(IEnumerable<Book> books);
}