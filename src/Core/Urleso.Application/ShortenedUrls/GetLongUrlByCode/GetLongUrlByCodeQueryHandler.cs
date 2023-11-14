using Urleso.Application.Abstractions.Data.Repositories;
using Urleso.Application.Abstractions.Messaging;
using Urleso.Domain.Results;
using Urleso.Domain.ShortenedUrls;

namespace Urleso.Application.ShortenedUrls.GetLongUrlByCode;

internal sealed class GetLongUrlByCodeQueryHandler(
        IShortenedUrlRepository shortenedUrlRepository
    )
    : IQueryHandler<GetLongUrlByCodeQuery, LongUrl?>
{
    public async Task<TypedResult<LongUrl?>> Handle(GetLongUrlByCodeQuery query, CancellationToken cancellationToken)
    {
        var shortenedUrlResult = await shortenedUrlRepository.GetByCodeOrDefaultAsync(query.Code, cancellationToken);
        if (!shortenedUrlResult.IsSuccess)
        {
            return TypedResult<LongUrl?>.Failure(shortenedUrlResult.Error);
        }

        var shortenedUrl = shortenedUrlResult.Value;
        return TypedResult<LongUrl?>.Success(shortenedUrl?.LongUrl);
    }
}