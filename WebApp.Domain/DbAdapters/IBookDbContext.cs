using Microsoft.EntityFrameworkCore;
using WebApp.Domain.Entity;

namespace WebApp.Domain.DbAdapters;

public interface IBookDbContext
{
    DbSet<Book> Books { get; set; }
}