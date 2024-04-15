using Microsoft.EntityFrameworkCore;
using WebApp.Domain.Entity;

namespace WebApp.Domain.DbAdapters;

public interface IReviewDbContext
{
    DbSet<Review> Reviews { get; set; }
}