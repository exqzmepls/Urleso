using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Urleso.Application.Abstractions.Messaging;
using Urleso.Presentation.Shared;

namespace Urleso.Presentation.RedirectShortenedUrl;

public static class RedirectShortenedUrlEndpoint
{
    public static RouteHandlerBuilder MapShortenedUrlRedirect(this WebApplication app)
    {
        return app.MapGet("", RedirectByCodeAsync);
    }

    private static async Task<Results<RedirectHttpResult, NotFound, BadRequest<ErrorDetails>>> RedirectByCodeAsync(
        [AsParameters] RedirectShortenedUrlByCodeRequest request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken
    )
    {
        var query = request.MapToGetLongUrlByCodeQuery();
        var queryResult = await sender.SendAsync(query, cancellationToken);
        if (!queryResult.IsSuccess)
        {
            var errorDetails = queryResult.Error.MapToErrorDetails();
            return TypedResults.BadRequest(errorDetails);
        }

        var longUrl = queryResult.Value;
        if (longUrl is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Redirect(longUrl.Value);
    }
}