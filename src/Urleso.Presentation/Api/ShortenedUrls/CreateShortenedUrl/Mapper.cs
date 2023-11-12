using Urleso.Application.ShortenedUrls.CreateShortenedUrl;
using Urleso.Domain.ShortenedUrls;

namespace Urleso.Presentation.Api.ShortenedUrls.CreateShortenedUrl;

internal static class Mapper
{
    public static CreateShortenedUrlCommand MapToCreateShortenedUrlCommand(this CreateShortenedUrlRequest request)
    {
        var createShortenedUrlCommand = new CreateShortenedUrlCommand
        {
            LongUrl = request.Options.LongUrl
        };
        return createShortenedUrlCommand;
    }

    public static ShortenedUrlDetails MapToShortenedUrlDetails(this ShortenedUrl shortenedUrl)
    {
        var shortenedUrlDetails = new ShortenedUrlDetails
        {
            UrlCode = shortenedUrl.Code.Value
        };
        return shortenedUrlDetails;
    }
}