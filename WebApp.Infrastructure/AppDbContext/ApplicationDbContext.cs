using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Domain.DbAdapters;
using WebApp.Domain.Entity;
using WebApp.Infrastructure.AppDbContext.Configs;

namespace WebApp.Infrastructure.AppDbContext;

public class ApplicationDbContext : IdentityDbContext<User>, IAuthorBookDbContext, IAuthorDbContext,
    IBookDbContext, IReviewDbContext, ISaveDbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<AuthorBook> AuthorBooks { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AuthorConfig).Assembly);
        builder.Entity(typeof(AuthorBook)).ToTable("author_book");
        base.OnModelCreating(builder);
    }
}
