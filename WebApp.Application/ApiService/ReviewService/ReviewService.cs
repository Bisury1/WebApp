using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Application.Common;
using WebApp.Application.DtoRequest.ReviewDtoRequest;
using WebApp.Application.DtoResponse.ReviewsDtoResponse;
using WebApp.Application.Mappers.Interfaces;
using WebApp.Domain.DbAdapters;
using WebApp.Domain.Entity;

namespace WebApp.Application.ApiService.ReviewService;

public class ReviewService(
    IReviewDbContext reviewDbContext,
    ISaveDbContext saveDbContext,
    UserManager<User> userManager,
    IReviewMapper reviewMapper,
    IBookDbContext bookDbContext) : IReviewService
{
    public async Task<DtoGetReviewResponse> GetReview(int id)
    {
        var review = await reviewDbContext.Reviews.AsNoTracking()
            .Include(r => r.User).FirstOrDefaultAsync(r => r.Id == id);
        if (review is null)
            ThrowHelper.ThrowRecordNotFoundException("Не найден отзыв");

        return reviewMapper.MapToReviewResponse(review!);
    }

    public async Task<int> CreateReview(DtoCreateReviewRequest createReviewRequest)
    {
        var userTask = userManager.FindByIdAsync(createReviewRequest.UserId.ToString());
        var bookTask = bookDbContext.Books.FirstOrDefaultAsync(b => b.Id == createReviewRequest.BookId);

        await Task.WhenAll(bookTask, userTask);
        var user = userTask.Result;
        var book = bookTask.Result;
        
        if(user is null)
            ThrowHelper.ThrowRecordNotFoundException("Пользователь не найден");
        if(book is null)
            ThrowHelper.ThrowRecordNotFoundException("Книга не найдена");

        var existReview = await reviewDbContext.Reviews
            .Include(r => r.User).Include(r => r.Book)
            .FirstOrDefaultAsync(r => r.User == user && r.Book == book);
        if (existReview is not null)
            ThrowHelper.ThrowValidationException("Отзыв на эту книгу уже был оставлен");
        
        var review = reviewMapper.MapToReview(createReviewRequest, user!, book!);
        var addedReview =  await reviewDbContext.Reviews.AddAsync(review);
        await saveDbContext.SaveChangesAsync(CancellationToken.None);
        return addedReview.Entity.Id;
    }

    public async Task<bool> DeleteReview(DtoDeleteReviewRequest deleteReviewRequest)
    {
        var review = await reviewDbContext.Reviews.FirstOrDefaultAsync(r => r.Id == deleteReviewRequest.Id);
        if (review is null)
            return false;

        reviewDbContext.Reviews.Remove(review);
        await saveDbContext.SaveChangesAsync(CancellationToken.None);
        return true;
    }
}