using Microsoft.AspNetCore.Mvc;

namespace Urleso.Presentation.RedirectShortenedUrl;

public sealed class RedirectShortenedUrlByCodeRequest
{
    [FromRoute]
    public required string UrlCode { get; init; }
}