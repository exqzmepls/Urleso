using Urleso.SharedKernel.Results;

namespace Urleso.Domain.ShortenedUrls;

public sealed record UrlCode
{
    public const int CodeLength = 8;

    // https://datatracker.ietf.org/doc/html/rfc3548#section-4
    // Base64 URL safe encoding Alphabet -> '+' and '/' are replaced with '-' and '_'
    public const string CodeAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";

    private UrlCode(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static TypedResult<UrlCode> Create(string code)
    {
        if (!IsLengthCorrect(code))
        {
            return Errors.UrlCode.WrongLength;
        }

        if (!IsConsistOfCodeAlphabetChars(code))
        {
            return Errors.UrlCode.InvalidChars;
        }

        return new UrlCode(code);
    }

    private static bool IsLengthCorrect(string code) => code.Length == CodeLength;

    private static bool IsConsistOfCodeAlphabetChars(string code) => code.All(c => CodeAlphabet.Contains(c));
}