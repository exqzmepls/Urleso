using Urleso.Domain.Results;

namespace Urleso.Domain.ShortenedUrls;

public static class Errors
{
    public static class UrlCode
    {
        public static readonly Error WrongLength = new("UrlCode.WrongLength", "Code has wrong length.");

        public static readonly Error InvalidChars = new("UrlCode.InvalidChars", "Code has invalid chars.");
    }

    public static class LongUrl
    {
        public static readonly Error LengthTooLong = new("LongUrl.LengthTooLong", "Url is very long.");

        public static readonly Error NonAbsoluteLink = new("LongUrl.NonAbsoluteLink", "Url is not absolute.");
    }
}