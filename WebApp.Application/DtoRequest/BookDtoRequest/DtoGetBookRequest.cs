using WebApp.Application.Common;

namespace WebApp.Application.DtoRequest.BookDtoRequest;

public class DtoGetBookRequest
{
    private DtoGetBookRequest(int id)
    {
        Id = id;
    }

    public int Id { get; }
    
    public static Result<DtoGetBookRequest, string> Create(int id)
    {
        return new DtoGetBookRequest(id);
    }
}