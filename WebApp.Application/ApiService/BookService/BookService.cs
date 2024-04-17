using Microsoft.EntityFrameworkCore;
using WebApp.Application.Common;
using WebApp.Application.DtoRequest.BookDtoRequest;
using WebApp.Application.DtoResponse.BookDtoResponse;
using WebApp.Application.Mappers.Interfaces;
using WebApp.Domain.DbAdapters;
using WebApp.Domain.Entity;

namespace WebApp.Application.ApiService.BookService;

public class BookService(
    IBookDbContext bookDbContext,
    IReviewDbContext reviewDbContext,
    ISaveDbContext saveDbContext,
    IAuthorDbContext authorDbContext,
    IBookMapper bookMapper)
    : IBookService
{
    private const string NotFoundBookMessage = "Не найдена запись книги с id: {0}";

    public async Task<DtoGetBookResponse> GetBook(DtoGetBookRequest getBookRequest)
    {
        var bookId = getBookRequest.Id;
        var book = await bookDbContext.Books.AsNoTracking()
            .Include(b => b.Authors)
            .Include(b => b.Reviews).ThenInclude(r => r.User)
            .FirstOrDefaultAsync(b => b.Id == bookId);

        if (book is null)
        {
            ThrowHelper.ThrowRecordNotFoundException(string.Format(NotFoundBookMessage, bookId));
        }
        
        return bookMapper.MapToGetBookResponse(book!);
    }
    
    public async Task<DtoGetBooksResponse> GetBooks()
    {
        var books = await bookDbContext.Books.AsNoTracking()
            .Include(b => b.Authors).ToListAsync();
        return new DtoGetBooksResponse
        {
            Books = bookMapper.MapToGetBookResponses(books)
        };
    }

    public async Task<DtoGetBooksResponse> GetBooksByTitle(DtoGetBookByTitleRequest getBookByTitleRequest)
    {
        var books = await bookDbContext.Books.AsNoTracking().Include(b => b.Authors)
            .Where(b => b.Title.Contains(getBookByTitleRequest.Title)).ToListAsync();
        return new DtoGetBooksResponse
        {
            Books = bookMapper.MapToGetBookResponses(books)
        };
    }

    public async Task<int> CreateBook(DtoCreateBookRequest createBookRequest)
    {
        var authorsFromRequest = createBookRequest.AuthorsIds;
        var authors = await authorDbContext.Authors.Where(a => authorsFromRequest.Contains(a.Id)).ToListAsync();
        if (authors.Count < authorsFromRequest.Count())
            ThrowHelper.ThrowValidationException("Авторы с переданными идентификаторами не найдены");

        var addedBook = new Book
        {
            Title = createBookRequest.Title,
            Authors = authors,
            Reviews = Array.Empty<Review>()
        };
        var added = await bookDbContext.Books.AddAsync(addedBook);
        await saveDbContext.SaveChangesAsync(CancellationToken.None);
        return added.Entity.Id;
    }

    public async Task<bool> UpdateTitle(DtoUpdateBookTitleRequest updateBookTitleRequest)
    {
        var bookId = updateBookTitleRequest.Id;
        var book = await bookDbContext.Books.FindAsync([bookId]);
        if (book is null)
            ThrowHelper.ThrowRecordNotFoundException(string.Format(NotFoundBookMessage, bookId));

        book!.Title = updateBookTitleRequest.Title;
        await saveDbContext.SaveChangesAsync(CancellationToken.None);
        return true;
    }

    public async Task<bool> UpdateBook(DtoUpdateBookRequest updateBookRequest)
    {
        var authorsFromRequest = updateBookRequest.AuthorsIds;
        var authors = await authorDbContext.Authors.Where(a => authorsFromRequest.Contains(a.Id)).ToListAsync();
        if (authors.Count < authorsFromRequest.Count())
            ThrowHelper.ThrowValidationException("Авторы с переданными идентификаторами не найдены");

        var reviewsFromRequest = updateBookRequest.ReviewsIds;
        var reviews = await reviewDbContext.Reviews.Where(r => reviewsFromRequest.Contains(r.Id)).ToListAsync();
        if (reviews.Count < reviewsFromRequest.Count())
            ThrowHelper.ThrowValidationException("Отзывы с переданными идентификаторами не найдены");
        
        var bookId = updateBookRequest.Id;
        var book = await bookDbContext.Books.FindAsync([bookId]);
        if (book is null)
            ThrowHelper.ThrowRecordNotFoundException(string.Format(NotFoundBookMessage, bookId));
        
        book!.Reviews = reviews;
        book.Authors = authors;
        book.Title = updateBookRequest.Title;
        await saveDbContext.SaveChangesAsync(CancellationToken.None);
        return true;
    }

    public async Task<bool> DeleteBook(DtoDeleteBookRequest deleteBookRequest)
    {
        var book = await bookDbContext.Books.FindAsync(deleteBookRequest.Id);
        if (book is null)
            return false;

        bookDbContext.Books.Remove(book);
        await saveDbContext.SaveChangesAsync(CancellationToken.None);
        return true;
    }
}