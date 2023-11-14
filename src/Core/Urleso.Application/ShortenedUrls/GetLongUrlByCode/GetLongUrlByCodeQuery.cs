using Urleso.Application.Abstractions.Messaging;
using Urleso.Domain.ShortenedUrls;

namespace Urleso.Application.ShortenedUrls.GetLongUrlByCode;

public sealed class GetLongUrlByCodeQuery : IQuery<LongUrl?>
{
    public required string Code { get; init; }
}