using Urleso.Domain.ShortenedUrls;
using Urleso.SharedKernel.Results;

namespace Urleso.Application.Abstractions.Data.Repositories;

public interface IShortenedUrlRepository
{
    public Task<TypedResult<bool>> IsCodeExistAsync(UrlCode code, CancellationToken cancellationToken);

    public Task<Result> AddAsync(ShortenedUrl url, CancellationToken cancellationToken);
}