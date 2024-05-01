using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Urleso.Redirect.Application;

namespace Urleso.Redirect.Presentation;

internal static class ShortenedUrlRedirectEndpoint
{
    public static RouteHandlerBuilder MapShortenedUrlRedirect(this WebApplication app) =>
        app.MapGet("/{UrlCode}", RedirectAsync);

    private static async Task<Results<RedirectHttpResult, NotFound>> RedirectAsync(
        [AsParameters] RedirectShortenedUrlRequest redirectRequest,
        [FromServices] GetLongUrlQuery longUrlQuery,
        CancellationToken cancellationToken)
    {
        var longUrl = await longUrlQuery.ExecuteAsync(redirectRequest.UrlCode, cancellationToken);
        return longUrl is null ? TypedResults.NotFound() : TypedResults.Redirect(longUrl);
    }
}