using Microsoft.EntityFrameworkCore;
using WebApp.Domain.Entity;

namespace WebApp.Domain.DbAdapters;

public interface IAuthorDbContext
{
    DbSet<Author> Authors { get; set; }
}