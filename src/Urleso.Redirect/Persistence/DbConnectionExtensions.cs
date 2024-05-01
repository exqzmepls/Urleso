using System.Data;
using Dapper;

namespace Urleso.Redirect.Persistence;

internal static class DbConnectionExtensions
{
    public static async Task<ShortenedUrlResponse?> QueryShortenedUrlAsync(
        this IDbConnection dbConnection,
        string urlCode,
        CancellationToken cancellationToken)
    {
        var queryCommand = new CommandDefinition(
            commandText: """SELECT "Id", "LongUrl" FROM "ShortenedUrl" WHERE "Code" = @UrlCode LIMIT 2""",
            parameters: new
            {
                UrlCode = urlCode
            },
            cancellationToken: cancellationToken
        );
        var response = await dbConnection.QuerySingleOrDefaultAsync<ShortenedUrlResponse>(queryCommand);
        return response;
    }
}