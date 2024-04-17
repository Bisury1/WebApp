using Microsoft.Extensions.DependencyInjection;
using WebApp.Application.ApiService.AuthorService;
using WebApp.Application.ApiService.BookService;
using WebApp.Application.ApiService.ReviewService;
using WebApp.Application.Mappers;
using WebApp.Application.Mappers.Interfaces;

namespace WebApp.Application.DI;

public static partial class DependencyInjectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IReviewService, ReviewService>();
        serviceCollection.AddScoped<IAuthorService, AuthorService>();
        serviceCollection.AddScoped<IBookService, BookService>();
        serviceCollection.AddScoped<IAuthorMapper, AuthorMapper>();
        serviceCollection.AddScoped<IBookMapper, BookMapper>();
        serviceCollection.AddScoped<IReviewMapper, ReviewMapper>();
        serviceCollection.AddScoped<IUserMapper, UserMapper>();

        return serviceCollection;
    }
}