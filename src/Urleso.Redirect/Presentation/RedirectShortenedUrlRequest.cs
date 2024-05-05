using Microsoft.AspNetCore.Mvc;

namespace Urleso.Redirect.Presentation;

public sealed record RedirectShortenedUrlRequest
{
    [FromRoute(Name = "UrlCode")]
    public required string UrlCode { get; init; }
}