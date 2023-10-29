using Urleso.Domain.Results;
using Urleso.Domain.ShortenedUrls;

namespace Urleso.Application.Abstractions.Data.Repositories;

public interface IShortenedUrlRepository
{
    public Task<TypedResult<bool>> IsCodeExistAsync(UrlCode code, CancellationToken cancellationToken);

    public Task<TypedResult<ShortenedUrl?>> GetByCodeAsync(string code, CancellationToken cancellationToken);
}