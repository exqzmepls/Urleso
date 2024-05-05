using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Urleso.Application.Abstractions.Messaging;
using Urleso.Application.ShortenedUrls.CreateShortenedUrl;
using Urleso.Presentation.Api.ShortenedUrls.CreateShortenedUrl;
using Urleso.Presentation.Shared;

namespace Urleso.Presentation.Api.ShortenedUrls;

public sealed class ShortenedUrlEndpoints : BaseModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var shortenedUrlsGroup = app
            .MapGroup("shortened-urls")
            .WithTags("Shortened URLs");

        shortenedUrlsGroup.MapPost("/", CreateShortenedUrlAsync);
    }

    private static async Task<Results<Ok<ShortenedUrlDetails>, BadRequest<ErrorDetails>>> CreateShortenedUrlAsync(
        [AsParameters] CreateShortenedUrlRequest request,
        [FromServices] ISender sender,
        [FromServices] IOptions<RedirectSettings> redirectSettingsOptions,
        CancellationToken cancellationToken)
    {
        var command = new CreateShortenedUrlCommand
        {
            LongUrl = request.Options.LongUrl
        };
        var commandResult = await sender.SendAsync(command, cancellationToken);
        if (!commandResult.IsSuccess)
        {
            var errorDetails = commandResult.Error.MapToErrorDetails();
            return TypedResults.BadRequest(errorDetails);
        }

        var shortenedUrlCode = commandResult.Value.Code.Value;
        var shortenedUrlDetails = new ShortenedUrlDetails
        {
            Url = redirectSettingsOptions.Value.BaseAddress + shortenedUrlCode,
            UrlCode = shortenedUrlCode
        };
        return TypedResults.Ok(shortenedUrlDetails);
    }
}