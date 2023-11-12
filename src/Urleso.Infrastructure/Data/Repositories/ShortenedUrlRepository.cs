using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Urleso.Application.Abstractions.Data.Repositories;
using Urleso.Domain.Results;
using Urleso.Domain.ShortenedUrls;
using Urleso.Persistence;

namespace Urleso.Infrastructure.Data.Repositories;

internal sealed class ShortenedUrlRepository(
        ApplicationDbContext dbContext,
        ILogger<ShortenedUrlRepository> logger
    )
    : IShortenedUrlRepository
{
    private DbSet<ShortenedUrl> ShortenedUrls => dbContext.Set<ShortenedUrl>();

    public async Task<TypedResult<bool>> IsCodeExistAsync(UrlCode code, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting any shortened url with code '{UrlCode}'...", code);
        var isCodeExist = await ShortenedUrls.AnyAsync(u => u.Code == code, cancellationToken);
        logger.LogInformation("Are any shortened url with code '{UrlCode}': {IsUrlCodeExist}", code, isCodeExist);
        return TypedResult<bool>.Success(isCodeExist);
    }

    public async Task<Result> AddAsync(ShortenedUrl url, CancellationToken cancellationToken)
    {
        var longUrl = url.LongUrl;
        var urlCode = url.Code;
        logger.LogInformation("Adding shortened url for '{LongUrl}' with code '{UrlCode}'...", longUrl, urlCode);
        await ShortenedUrls.AddAsync(url, cancellationToken);
        logger.LogInformation("Shortened url for '{LongUrl}' with code '{UrlCode}' is added", longUrl, urlCode);
        return Result.Success();
    }

    public async Task<TypedResult<ShortenedUrl?>> GetByCodeOrDefaultAsync(string code,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting single shortened url with code '{UrlCode}'...", code);
        var shortenedUrl = await ShortenedUrls.SingleOrDefaultAsync(u => u.Code.Value == code, cancellationToken);
        logger.LogInformation("Shortened url with code '{UrlCode}' is for '{LongUrl}'", code, shortenedUrl?.LongUrl);
        return TypedResult<ShortenedUrl?>.Success(shortenedUrl);
    }
}