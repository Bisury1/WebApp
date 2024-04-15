using WebApp.Application.Common;

namespace WebApp.Application.DtoRequest.BookDtoRequest;

public class DtoDeleteBookRequest
{

    private DtoDeleteBookRequest(int id)
    {
        Id = id;
    }

    public int Id { get; }
    
    public static Result<DtoDeleteBookRequest, string> Create(int id)
    {
        return new DtoDeleteBookRequest(id);
    }
}