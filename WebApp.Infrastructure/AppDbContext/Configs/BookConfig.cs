using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Domain.Entity;

namespace WebApp.Infrastructure.AppDbContext.Configs;

public class BookConfig: IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);

        builder.HasMany(b => b.Authors).WithMany(a => a.Books)
            .UsingEntity<AuthorBook>(
                b => b.HasOne<Author>().WithMany().HasForeignKey(e => e.AuthorId),
                b => b.HasOne<Book>().WithMany().HasForeignKey(e => e.BookId));
        builder.HasMany(b => b.Reviews).WithOne(r => r.Book);

        builder.ToTable("book");
    }
}