using Urleso.Domain.Results;
using Urleso.Domain.ShortenedUrls;

namespace Urleso.Application.Abstractions.Data.Repositories;

public interface IShortenedUrlRepository
{
    public Task<TypedResult<bool>> IsCodeExistAsync(UrlCode code, CancellationToken cancellationToken);

    public Task<Result> AddAsync(ShortenedUrl url, CancellationToken cancellationToken);

    public Task<TypedResult<ShortenedUrl?>> GetByCodeOrDefaultAsync(UrlCode code, CancellationToken cancellationToken);
}