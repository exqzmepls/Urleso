using System.Data;
using Urleso.Redirect.Persistence;

namespace Urleso.Redirect.Application;

internal sealed class GetLongUrlQuery(
    IDbConnection dbConnection,
    ILogger<GetLongUrlQuery> logger
)
{
    public async Task<string?> ExecuteAsync(string urlCode, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting long URL for {@UrlCode}...", urlCode);
        var shortenedUrl = await dbConnection.QueryShortenedUrlAsync(urlCode, cancellationToken);

        if (shortenedUrl is null)
        {
            logger.LogInformation("Long URL for {@UrlCode} does not exist", urlCode);
            return null;
        }

        var longUrl = shortenedUrl.LongUrl;
        logger.LogInformation("Long URL for {@UrlCode} is {@LongUrl}", urlCode, longUrl);
        return longUrl;
    }
}