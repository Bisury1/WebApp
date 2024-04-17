using WebApp.Application.Common;

namespace WebApp.Application.DtoRequest.ReviewDtoRequest;

public class DtoCreateReviewRequest
{
    public string Title { get; init; }
    public string? Content { get; set; }
    public byte Grade { get; init; }
    public Guid UserId { get; init; }
    public int BookId { get; init; }

    private DtoCreateReviewRequest(string title, string? content, byte grade, int bookId, Guid userId)
    {
        Title = title;
        Content = content;
        Grade = grade;
        BookId = bookId;
        UserId = userId;
    }
    
    public static Result<DtoCreateReviewRequest, string> Create(string title, string? content, byte grade, int bookId, Guid userId)
    {
        if (Guid.Empty == userId)
        {
            return "Введен неверный идентификатор пользователя";
        }

        if (string.IsNullOrEmpty(title))
            return "Введено неверный заголовок отзыва";

        if (bookId < 0)
            return "Введен неверный идентификатор книги";
        
        if (grade is < 1 or > 5)
        {
            return "Оценка должна быть от 1 до 5";
        }
        
        return new DtoCreateReviewRequest(title, content, grade, bookId, userId);
    }
}