using Urleso.Application.ShortenedUrls.GetLongUrlByCode;

namespace Urleso.Presentation.RedirectShortenedUrl;

internal static class Mapper
{
    public static GetLongUrlByCodeQuery MapToGetLongUrlByCodeQuery(this RedirectShortenedUrlByCodeRequest request)
    {
        var getLongUrlByCodeQuery = new GetLongUrlByCodeQuery
        {
            Code = request.UrlCode
        };
        return getLongUrlByCodeQuery;
    }
}