using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Application.ApiService.BookService;
using WebApp.Application.DtoRequest.BookDtoRequest;
using WebApp.Application.DtoResponse.BookDtoResponse;
using WebApp.Common;
using WebApp.Domain.Enum;
using WebApp.Models;

namespace WebApp.Controllers;

[Authorize]
public class BookV1Controller(IBookService bookService) : ControllerBase
{
    /// <summary>
    /// Получение книги по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор кники</param>
    /// <returns></returns>
    [HttpGet]
    [Route("/v1/books/{id:int}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(DtoGetBookResponse), (int)HttpStatusCode.OK)]
    public async ValueTask<IActionResult> GetBookByIdAsync([FromRoute] int id)
    {
        var getRequestCreateResult = DtoGetBookRequest.Create(id);
        
        return getRequestCreateResult.IsSuccess 
            ? Ok(await bookService.GetBook(getRequestCreateResult.Value)) 
            : BadRequest(getRequestCreateResult.Error);
    }

    /// <summary>
    /// Получение всех книг
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("/v1/books")]
    [RecordNotFoundFilter]
    [ProducesResponseType(typeof(DtoGetBooksResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBooks()
    {
        return Ok(await bookService.GetBooks());
    }
    
    /// <summary>
    /// Получение книг по похожему названию
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("/v1/books/by-title/{title}")]
    [RecordNotFoundFilter]
    [ProducesResponseType(typeof(DtoGetBooksResponse), (int)HttpStatusCode.OK)]
    public async ValueTask<IActionResult> GetBooksByTitle(string title)
    {
        var getBookByTitleRequest = DtoGetBookByTitleRequest.Create(title);
        
        return getBookByTitleRequest.IsSuccess 
            ? Ok(await bookService.GetBooksByTitle(getBookByTitleRequest.Value)) 
            : BadRequest(getBookByTitleRequest.Error);
    }

    /// <summary>
    /// Создание книги (для администратора)
    /// </summary>
    /// <param name="createBookRequest">Данные для создания книги</param>
    /// <returns></returns>
    [HttpPost]
    [Route("/v1/books/create")]
    [Authorize(Roles = nameof(UserRoles.Admin))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    public async ValueTask<IActionResult> CreateBook([FromBody] CreateBookRequest createBookRequest)
    {
        var createRequestResult = DtoCreateBookRequest.Create(createBookRequest.Title, createBookRequest.AuthorsIds);
        
        return createRequestResult.IsSuccess 
            ? Ok(await bookService.CreateBook(createRequestResult.Value))
            : BadRequest(createRequestResult.Error);
    }
    
    /// <summary>
    /// Полное обновление данных книги (для администратора)
    /// </summary>
    /// <param name="id">Идентификатор обновляемой книги</param>
    /// <param name="updateBookRequest">Данные, которые будут после обновления</param>
    /// <returns></returns>
    [HttpPut]
    [RecordNotFoundFilter]
    [Route("/v1/books/{id:int}")]
    [Authorize(Roles = nameof(UserRoles.Admin))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    public async ValueTask<IActionResult> UpdateBook([FromRoute] int id, [FromBody] UpdateBookRequest updateBookRequest)
    {
        var updateRequestResult = DtoUpdateBookRequest.Create(id, updateBookRequest.Title,
            updateBookRequest.AuthorsIds, updateBookRequest.ReviewsIds);
        
        return updateRequestResult.IsSuccess 
            ? Ok(await bookService.UpdateBook(updateRequestResult.Value))
            : BadRequest(updateRequestResult.Error);
    }

    /// <summary>
    /// Обновление названия книги (для администратора)
    /// </summary>
    /// <param name="id">идентификатор книги</param>
    /// <param name="updateBookTitleRequest">Новое название книги</param>
    /// <returns></returns>
    [HttpPatch]
    [RecordNotFoundFilter]
    [Route("/v1/books/{id:int}")]
    [Authorize(Roles = nameof(UserRoles.Admin))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    public async ValueTask<IActionResult> UpdateBookTitle([FromRoute] int id,
        [FromBody] UpdateBookTitleRequest updateBookTitleRequest)
    {
        var updateRequestResult =
            DtoUpdateBookTitleRequest.Create(id, updateBookTitleRequest.Title);

        return updateRequestResult.IsSuccess
            ? Ok(await bookService.UpdateTitle(updateRequestResult.Value))
            : BadRequest(updateRequestResult.Error);
    }

    /// <summary>
    /// Удаление книги (для администратора)
    /// </summary>
    /// <param name="id">Идентификатор удаляемой книги</param>
    /// <returns></returns>
    [HttpDelete]
    [Route("/v1/books/{id:int}")]
    [Authorize(Roles = nameof(UserRoles.Admin))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    public async ValueTask<IActionResult> DeleteBook([FromRoute] int id)
    {
        var deleteRequestResult = DtoDeleteBookRequest.Create(id);
        
        return deleteRequestResult.IsSuccess 
            ? Ok(await bookService.DeleteBook(deleteRequestResult.Value))
            : BadRequest(deleteRequestResult.Error);
    }
}