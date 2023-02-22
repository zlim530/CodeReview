using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCoreModel;

internal class BookConfig : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("T_Books").Property(b => b.Title).HasMaxLength(500).IsRequired();

        builder.Property(b => b.AuthorName).HasMaxLength(50).IsRequired();
    }
}