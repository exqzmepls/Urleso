using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Urleso.Domain.ShortenedUrls;

namespace Urleso.Persistence.Configurations;

internal sealed class ShortenedUrlConfiguration : IEntityTypeConfiguration<ShortenedUrl>
{
    public void Configure(EntityTypeBuilder<ShortenedUrl> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .HasConversion(shortenedUrlId => shortenedUrlId.Value, value => new ShortenedUrlId(value));

        builder.Property(u => u.CreatedOnUtc);

        builder.Property(u => u.LongUrl)
            .HasMaxLength(LongUrl.UrlMaxLength)
            .HasConversion(longUrl => longUrl.Value, value => LongUrl.Create(value).Value);

        builder.HasUniqueIndex(u => u.Code);
        builder.Property(u => u.Code)
            .HasMaxLength(UrlCode.CodeLength)
            .HasConversion(urlCode => urlCode.Value, value => UrlCode.Create(value).Value);
    }
}