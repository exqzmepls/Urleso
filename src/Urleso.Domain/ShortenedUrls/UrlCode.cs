using Urleso.Domain.Results;

namespace Urleso.Domain.ShortenedUrls;

public sealed record UrlCode
{
    private const int CodeDefaultLength = 8;

    private static readonly HashSet<char> AvailableCharsSet = GetAvailableCharsSet();

    private UrlCode(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static TypedResult<UrlCode> Create(string code)
    {
        if (!IsLengthCorrect(code))
        {
            return TypedResult<UrlCode>.Failure(Errors.UrlCode.WrongLength);
        }

        if (!AreCharsValid(code))
        {
            return TypedResult<UrlCode>.Failure(Errors.UrlCode.InvalidChars);
        }

        return TypedResult<UrlCode>.Success(new UrlCode(code));
    }

    private static bool IsLengthCorrect(string code) => code.Length == CodeDefaultLength;

    private static bool AreCharsValid(string code) => code.All(c => AvailableCharsSet.Contains(c));

    private static HashSet<char> GetAvailableCharsSet()
    {
        var result = new HashSet<char>();
        AddChars(result, 'A', 'Z');
        AddChars(result, 'a', 'z');
        AddChars(result, '0', '9');
        return result;

        void AddChars(ISet<char> source, char start, char end)
        {
            var count = end - start + 1;
            var chars = Enumerable.Range(start, count).Select(Convert.ToChar);
            foreach (var c in chars)
            {
                source.Add(c);
            }
        }
    }
}