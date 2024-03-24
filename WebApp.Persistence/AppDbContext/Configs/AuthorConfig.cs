using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Persistence.Model;

namespace WebApp.Persistence.AppDbContext.Configs;

public class AuthorConfig: IEntityTypeConfiguration<DbAuthor>
{
    public void Configure(EntityTypeBuilder<DbAuthor> builder)
    {
        builder.HasKey(author => author.Id);

        builder.ToTable("author");
    }
}