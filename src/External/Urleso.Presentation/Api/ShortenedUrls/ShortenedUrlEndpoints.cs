using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Urleso.Application.Abstractions.Messaging;
using Urleso.Presentation.Api.ShortenedUrls.CreateShortenedUrl;
using Urleso.Presentation.Shared;

namespace Urleso.Presentation.Api.ShortenedUrls;

public sealed class ShortenedUrlEndpoints : BaseModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var shortenedUrlsGroup = app.MapGroup("shortened-urls");

        shortenedUrlsGroup.MapPost("", CreateShortenedUrlAsync);
    }

    private static async Task<Results<Ok<ShortenedUrlDetails>, BadRequest<ErrorDetails>>> CreateShortenedUrlAsync(
        [AsParameters] CreateShortenedUrlRequest request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken
    )
    {
        var command = request.MapToCreateShortenedUrlCommand();
        var commandResult = await sender.SendAsync(command, cancellationToken);
        if (!commandResult.IsSuccess)
        {
            var errorDetails = commandResult.Error.MapToErrorDetails();
            return TypedResults.BadRequest(errorDetails);
        }

        var shortenedUrlDetails = commandResult.Value.MapToShortenedUrlDetails();
        return TypedResults.Ok(shortenedUrlDetails);
    }
}