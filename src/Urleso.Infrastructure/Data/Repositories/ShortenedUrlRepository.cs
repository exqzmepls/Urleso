using Microsoft.EntityFrameworkCore;
using Urleso.Application.Abstractions.Data.Repositories;
using Urleso.Domain.Results;
using Urleso.Domain.ShortenedUrls;
using Urleso.Persistence;

namespace Urleso.Infrastructure.Data.Repositories;

internal sealed class ShortenedUrlRepository(
        ApplicationDbContext dbContext
    )
    : IShortenedUrlRepository
{
    private DbSet<ShortenedUrl> ShortenedUrls => dbContext.Set<ShortenedUrl>();

    public async Task<TypedResult<bool>> IsCodeExistAsync(UrlCode code, CancellationToken cancellationToken)
    {
        var isCodeExist = await ShortenedUrls.AnyAsync(u => u.Code == code, cancellationToken);
        return TypedResult<bool>.Success(isCodeExist);
    }

    public async Task<Result> AddAsync(ShortenedUrl url, CancellationToken cancellationToken)
    {
        await ShortenedUrls.AddAsync(url, cancellationToken);
        return Result.Success();
    }

    public async Task<TypedResult<ShortenedUrl?>> GetByCodeOrDefaultAsync(string code,
        CancellationToken cancellationToken)
    {
        var shortenedUrl = await ShortenedUrls.SingleOrDefaultAsync(u => u.Code.Value == code, cancellationToken);
        return TypedResult<ShortenedUrl?>.Success(shortenedUrl);
    }
}