using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Urleso.Application.Abstractions.Data.Repositories;
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

    public async Task AddAsync(ShortenedUrl url, CancellationToken cancellationToken)
    {
        logger.LogInformation("Adding shortened url {@ShortenedUrl}...", url);
        await ShortenedUrls.AddAsync(url, cancellationToken);
        logger.LogInformation("Shortened url {@ShortenedUrl} is added", url);
    }
}