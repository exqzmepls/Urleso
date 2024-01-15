using Microsoft.AspNetCore.Mvc;

namespace Urleso.Presentation.Api.ShortenedUrls.CreateShortenedUrl;

public sealed class CreateShortenedUrlRequest
{
    [FromBody]
    public required ShortenedUrlOptions Options { get; init; }
}