using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Domain.DbAdapters;
using WebApp.Domain.Entity;
using WebApp.Infrastructure.AppDbContext;

namespace WebApp.Infrastructure.DI;

public static partial class DependencyInjectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionString"];
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        serviceCollection.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        serviceCollection.AddScoped<IAuthorDbContext>(serv => serv.GetRequiredService<ApplicationDbContext>());
        serviceCollection.AddScoped<IAuthorBookDbContext>(serv => serv.GetRequiredService<ApplicationDbContext>());
        serviceCollection.AddScoped<IBookDbContext>(serv => serv.GetRequiredService<ApplicationDbContext>());
        serviceCollection.AddScoped<IReviewDbContext>(serv => serv.GetRequiredService<ApplicationDbContext>());
        serviceCollection.AddScoped<ISaveDbContext>(serv => serv.GetRequiredService<ApplicationDbContext>());

        return serviceCollection;
    }
}