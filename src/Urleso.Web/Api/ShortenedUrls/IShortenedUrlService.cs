using FluentResults;

namespace Urleso.Web.Api.ShortenedUrls;

public interface IShortenedUrlService
{
    public Task<Result<string>> ShortenUrlAsync(string longUrl, CancellationToken cancellationToken = default);
}