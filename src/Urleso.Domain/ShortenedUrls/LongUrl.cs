using Urleso.Domain.Results;

namespace Urleso.Domain.ShortenedUrls;

public sealed record LongUrl
{
    public const int UrlMaxLength = 1024;

    private LongUrl(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static TypedResult<LongUrl> Create(string url)
    {
        if (!IsLengthNotGreaterThenMaxValue(url))
        {
            return TypedResult<LongUrl>.Failure(Errors.LongUrl.LengthTooLong);
        }

        if (!IsUrlAbsolute(url))
        {
            return TypedResult<LongUrl>.Failure(Errors.LongUrl.NonAbsoluteLink);
        }

        return TypedResult<LongUrl>.Success(new LongUrl(url));
    }

    private static bool IsLengthNotGreaterThenMaxValue(string url) => url.Length <= UrlMaxLength;

    private static bool IsUrlAbsolute(string url) => Uri.TryCreate(url, UriKind.Absolute, out _);
}