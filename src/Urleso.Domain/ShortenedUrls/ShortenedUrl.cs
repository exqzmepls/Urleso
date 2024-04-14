using Urleso.SharedKernel.Entities;

namespace Urleso.Domain.ShortenedUrls;

public sealed class ShortenedUrl : Entity<ShortenedUrlId>
{
    private ShortenedUrl(ShortenedUrlId id, LongUrl longUrl, UrlCode code, DateTime createdOnUtc) : base(id)
    {
        LongUrl = longUrl;
        Code = code;
        CreatedOnUtc = createdOnUtc;
    }

    public LongUrl LongUrl { get; }
    public UrlCode Code { get; }
    public DateTime CreatedOnUtc { get; }

    public static ShortenedUrl Create(ShortenedUrlId id, LongUrl longUrl, UrlCode code, DateTime createdOnUtc)
    {
        return new ShortenedUrl(id, longUrl, code, createdOnUtc);
    }
}