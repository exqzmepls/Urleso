using Xunit;

namespace Urleso.Domain.UnitTests.ShortenedUrls.LongUrlValueObject;

internal sealed class LongUrlCreatePositiveTestData : TheoryData<string>
{
    public LongUrlCreatePositiveTestData()
    {
        WhenValid();
        WhenMaxLength();
        WhenOneButMaxLength();
    }

    private void WhenValid()
    {
        Add("https://example.com");
    }

    private void WhenMaxLength()
    {
        var longUrl = "https://example.com/" + new string('a', 1004);
        Add(longUrl);
    }

    private void WhenOneButMaxLength()
    {
        var longUrl = "https://example.com/" + new string('a', 1003);
        Add(longUrl);
    }
}