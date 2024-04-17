using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Application.ApiService.ReviewService;
using WebApp.Application.DtoRequest.ReviewDtoRequest;
using WebApp.Application.DtoResponse.ReviewsDtoResponse;
using WebApp.Common;
using WebApp.Domain.Enum;
using WebApp.Models;

namespace WebApp.Controllers;

public class ReviewV1Controller(IReviewService reviewService): ControllerBase
{
    private Guid UserId => !User.Identity!.IsAuthenticated
        ? Guid.Empty
        : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

    /// <summary>
    /// Получение отзыва по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("/v1/reviews/{id:int}")]
    [Authorize]
    [RecordNotFoundFilter]
    [ProducesResponseType(typeof(DtoGetReviewResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetReview([FromRoute] int id)
    {
        return Ok(await reviewService.GetReview(id));
    }

    /// <summary>
    /// Создание отзыва
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("/v1/reviews/create")]
    [Authorize]
    [RecordNotFoundFilter]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    public async ValueTask<IActionResult> CreateReview([FromBody] CreateReviewRequest request)
    {
        var createReviewRequest =
            DtoCreateReviewRequest.Create(request.Title, request.Content, request.Grade, request.BookId, UserId);

        return createReviewRequest.IsSuccess
            ? Ok(await reviewService.CreateReview(createReviewRequest.Value))
            : BadRequest(createReviewRequest.Error);
    }

    /// <summary>
    /// Удаление отзыва (только для администратора)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("/v1/reviews/delete/{id:int}")]
    [Authorize(Roles = nameof(UserRoles.Admin))]
    [RecordNotFoundFilter]
    public async ValueTask<IActionResult> CreateReview([FromRoute] int id)
    {
        var createReviewRequest =
            DtoDeleteReviewRequest.Create(id);

        return createReviewRequest.IsSuccess
            ? Ok(await reviewService.DeleteReview(createReviewRequest.Value))
            : BadRequest(createReviewRequest.Error);
    }
}