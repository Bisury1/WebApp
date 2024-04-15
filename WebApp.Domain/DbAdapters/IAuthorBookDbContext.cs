using Microsoft.EntityFrameworkCore;
using WebApp.Domain.Entity;

namespace WebApp.Domain.DbAdapters;

public interface IAuthorBookDbContext
{
    DbSet<AuthorBook> AuthorBooks { get; set; }
}