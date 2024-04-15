namespace WebApp.Domain.DbAdapters;

public interface ISaveDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}