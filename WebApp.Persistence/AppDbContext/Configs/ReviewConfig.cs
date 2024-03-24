using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Persistence.Model;

namespace WebApp.Persistence.AppDbContext.Configs;

public class ReviewConfig: IEntityTypeConfiguration<DbReview>
{
    public void Configure(EntityTypeBuilder<DbReview> builder)
    {
        builder.HasKey(r => r.Id);

        builder.ToTable("review");
    }
}