using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Application.ApiService.AuthorService;
using WebApp.Application.DtoRequest.AuthorDtoRequest;
using WebApp.Application.DtoResponse.AuthorDtoResponse;
using WebApp.Common;
using WebApp.Domain.Enum;
using WebApp.Models;

namespace WebApp.Controllers;

public class AuthorV1Controller(IAuthorService authorService) : ControllerBase
{
    /// <summary>
    /// Получение всех авторов
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    [Route("/v1/authors")]
    [RecordNotFoundFilter]
    [ProducesResponseType(typeof(DtoGetAuthorsResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAuthors()
    {
        return Ok(await authorService.GetAuthors());
    }
    
    /// <summary>
    /// Получение авторов по части псевдонима
    /// </summary>
    /// <param name="alias"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    [RecordNotFoundFilter]
    [Route("/v1/authors/by-alias/{alias}")]
    [ProducesResponseType(typeof(DtoGetAuthorsResponse), (int)HttpStatusCode.OK)]
    public async ValueTask<IActionResult> GetAuthors([FromRoute] string alias)
    {
        var getAuthorByAliasRequest = DtoGetAuthorsByAlias.Create(alias);
        
        return getAuthorByAliasRequest.IsSuccess
            ? Ok(await authorService.GetAuthors(getAuthorByAliasRequest.Value))
            : BadRequest(getAuthorByAliasRequest.Error);
    }

    /// <summary>
    /// Получение авторов по части псевдонима
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    [Route("/v1/authors/{id:int}")]
    [RecordNotFoundFilter]
    [ProducesResponseType(typeof(DtoGetAuthorResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAuthor([FromRoute] int id)
    {
        return Ok(await authorService.GetAuthor(id));
    }

    /// <summary>
    /// Создание автора (только для администратора)
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = nameof(UserRoles.Admin))]
    [Route("/v1/authors/create")]
    [RecordNotFoundFilter]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    public async ValueTask<IActionResult> CreateAuthors([FromBody] CreateAuthorRequest request)
    {
        var createAuthorRequest = DtoCreateAuthorRequest.Create(request.Alias);
        return createAuthorRequest.IsSuccess
            ? Ok(await authorService.CreateAuthor(createAuthorRequest.Value))
            : BadRequest(createAuthorRequest.Error);
    }

    /// <summary>
    /// Изменение псевдонима автора (только для администратора)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPatch]
    [Authorize(Roles = nameof(UserRoles.Admin))]
    [Route("/v1/authors/update-alias/{id:int}")]
    [RecordNotFoundFilter]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async ValueTask<IActionResult> ChangeAuthorAlias([FromRoute] int id, [FromBody] ChangeAuthorAliasRequest request)
    {
        var changeAuthorAliasRequest = DtoAuthorUpdateAuthorAliasRequest.Create(id, request.Alias);
        return changeAuthorAliasRequest.IsSuccess
            ? Ok(await authorService.ChangeAuthorAlias(changeAuthorAliasRequest.Value))
            : BadRequest(changeAuthorAliasRequest.Error);
    }
    
    /// <summary>
    /// Удаление автора (только для администратора)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Authorize(Roles = nameof(UserRoles.Admin))]
    [Route("/v1/authors/delete/{id:int}")]
    [RecordNotFoundFilter]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async ValueTask<IActionResult> DeleteAuthor([FromRoute] int id)
    {
        var deleteAuthorRequest = DtoDeleteAuthorRequest.Create(id);
        return deleteAuthorRequest.IsSuccess
            ? Ok(await authorService.DeleteAuthor(deleteAuthorRequest.Value))
            : BadRequest(deleteAuthorRequest.Error);
    }
}