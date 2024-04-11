using Urleso.Application.Abstractions.Data.Repositories;
using Urleso.Application.Abstractions.Messaging;
using Urleso.Domain.ShortenedUrls;
using Urleso.SharedKernel;

namespace Urleso.Application.ShortenedUrls.GetLongUrlByCode;

internal sealed class GetLongUrlByCodeQueryHandler(
    IShortenedUrlRepository shortenedUrlRepository
)
    : IQueryHandler<GetLongUrlByCodeQuery, LongUrl?>
{
    public async Task<TypedResult<LongUrl?>> Handle(GetLongUrlByCodeQuery query, CancellationToken cancellationToken)
    {
        var urlCodeResult = UrlCode.Create(query.Code);
        if (urlCodeResult.IsFailure)
        {
            return urlCodeResult.Error;
        }

        var urlCode = urlCodeResult.Value;
        var shortenedUrlResult = await shortenedUrlRepository.GetByCodeOrDefaultAsync(urlCode, cancellationToken);
        if (shortenedUrlResult.IsFailure)
        {
            return shortenedUrlResult.Error;
        }

        return shortenedUrlResult.Value?.LongUrl;
    }
}