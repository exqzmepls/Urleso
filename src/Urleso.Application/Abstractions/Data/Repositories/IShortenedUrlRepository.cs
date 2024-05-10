using Urleso.Domain.ShortenedUrls;

namespace Urleso.Application.Abstractions.Data.Repositories;

public interface IShortenedUrlRepository
{
    public Task AddAsync(ShortenedUrl url, CancellationToken cancellationToken);
}