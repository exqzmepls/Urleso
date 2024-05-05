using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Urleso.Application.Abstractions.Data.Repositories;
using Urleso.Domain.ShortenedUrls;
using Urleso.Persistence;
using Urleso.SharedKernel.Results;

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
        logger.LogInformation("Getting any shortened url with code {@UrlCode}...", code);
        var isCodeExist = await ShortenedUrls.AnyAsync(u => u.Code == code, cancellationToken);
        logger.LogInformation("Are any shortened url with code {@UrlCode}: {IsUrlCodeExist}", code, isCodeExist);
        return isCodeExist;
    }

    public async Task<Result> AddAsync(ShortenedUrl url, CancellationToken cancellationToken)
    {
        logger.LogInformation("Adding shortened url {@ShortenedUrl}...", url);
        await ShortenedUrls.AddAsync(url, cancellationToken);
        logger.LogInformation("Shortened url {@ShortenedUrl} is added", url);
        return Result.Success();
    }
}