using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Persistence.AppDbContext.Configs;
using WebApp.Persistence.Model;

namespace WebApp.Persistence.AppDbContext;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<DbAuthor> Authors { get; set; }
    public DbSet<DbBook> Books { get; set; }
    public DbSet<DbReview> Reviews { get; set; }
    public DbSet<DbUser> Users { get; set; }
    public DbSet<DbAuthorBook> AuthorBooks { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AuthorConfig).Assembly);
        builder.Entity(typeof(DbAuthorBook)).ToTable("author_book");
        base.OnModelCreating(builder);
    }
}