using FluentResults;
using Urleso.Api.Client;

namespace Urleso.Web.Api.ShortenedUrls;

internal sealed class ShortenedUrlService(
        IApiClientFactory apiClientFactory,
        ILogger<ShortenedUrlService> logger
    )
    : IShortenedUrlService
{
    public async Task<Result<string>> ShortenUrlAsync(string longUrl, CancellationToken cancellationToken = default)
    {
        var client = apiClientFactory.CreateClient();

        var shortenedUrlOptions = new ShortenedUrlOptions
        {
            LongUrl = longUrl
        };

        try
        {
            var shortenedUrlDetails = await client.PostApiShortenedUrlsAsync(shortenedUrlOptions, cancellationToken);
            return shortenedUrlDetails.Url;
        }
        catch (ApiException<ErrorDetails> apiException)
        {
            var errorDetails = apiException.Result;
            logger.LogError(apiException, "API request error details: {@ErrorDetails}", errorDetails);
            return Result.Fail<string>(apiException.Result.Description);
        }
        catch (ApiException apiException)
        {
            logger.LogError(apiException,
                "API error ({ApiErrorStatusCode}) with response: '{ApiErrorResponse}' and headers: '{@ApiErrorHeaders}'",
                apiException.StatusCode, apiException.Response, apiException.Headers
            );
            return Result.Fail<string>("Some error occurred :( please try again.");
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unexpected error: {@ApiUnexpectedError}", exception);
            return Result.Fail<string>("Something unexpected happened, ops...");
        }
    }
}