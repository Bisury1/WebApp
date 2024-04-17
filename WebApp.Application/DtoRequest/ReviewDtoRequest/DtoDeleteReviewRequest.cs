using WebApp.Application.Common;

namespace WebApp.Application.DtoRequest.ReviewDtoRequest;

public class DtoDeleteReviewRequest
{
    public int Id { get; init; }

    private DtoDeleteReviewRequest(int id)
    {
        Id = id;
    }
    
    public static Result<DtoDeleteReviewRequest, string> Create(int id)
    {
        if (id < 0)
        {
            return "Введен неверный идентификатор отзыва";
        }
        
        return new DtoDeleteReviewRequest(id);
    }
}