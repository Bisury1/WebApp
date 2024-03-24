using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Persistence.Model;

namespace WebApp.Persistence.AppDbContext.Configs;

public class BookConfig: IEntityTypeConfiguration<DbBook>
{
    public void Configure(EntityTypeBuilder<DbBook> builder)
    {
        builder.HasKey(b => b.Id);

        builder.HasMany(b => b.Authors).WithMany(a => a.Books)
            .UsingEntity<DbAuthorBook>(
                b => b.HasOne<DbAuthor>().WithMany().HasForeignKey(e => e.AuthorId),
                b => b.HasOne<DbBook>().WithMany().HasForeignKey(e => e.BookId));
        builder.HasMany(b => b.Reviews).WithOne(r => r.Book);

        builder.ToTable("book");
    }
}