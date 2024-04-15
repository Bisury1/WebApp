namespace WebApp.Application.DtoResponse.BookDtoResponse;

public class DtoGetBooksResponse
{
    public required IEnumerable<DtoGetBookLightResponse> Books { get; set; }
}