using WebApp.Application.DtoRequest.BookDtoRequest;
using WebApp.Application.DtoResponse.BookDtoResponse;

namespace WebApp.Application.ApiService.BookService;

public interface IBookService
{
    Task<DtoGetBookResponse> GetBook(DtoGetBookRequest getBookRequest);
    Task<DtoGetBooksResponse> GetBooks();
    Task<DtoGetBooksResponse> GetBooksByTitle(DtoGetBookByTitleRequest title);
    Task<int> CreateBook(DtoCreateBookRequest createBookRequest);
    Task<bool> UpdateTitle(DtoUpdateBookTitleRequest updateBookTitleRequest);
    Task<bool> UpdateBook(DtoUpdateBookRequest updateBookRequest);
    Task<bool> DeleteBook(DtoDeleteBookRequest deleteBookRequest);
}