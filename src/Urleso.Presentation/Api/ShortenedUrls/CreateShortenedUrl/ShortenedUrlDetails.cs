namespace Urleso.Presentation.Api.ShortenedUrls.CreateShortenedUrl;

public sealed class ShortenedUrlDetails
{
    public required string Url { get; init; }

    public required string UrlCode { get; init; }
}