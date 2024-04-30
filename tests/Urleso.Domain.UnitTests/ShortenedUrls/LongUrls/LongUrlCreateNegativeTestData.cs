using Urleso.Domain.ShortenedUrls;
using Urleso.SharedKernel.Results;

namespace Urleso.Domain.UnitTests.ShortenedUrls.LongUrls;

internal sealed class LongUrlCreateNegativeTestData : TheoryData<string, Error>
{
    public LongUrlCreateNegativeTestData()
    {
        WhenEmptyString();
        WhenNotAbsolute();
        WhenTooLong();
    }

    private void WhenEmptyString()
    {
        Add(string.Empty, Errors.LongUrl.NonAbsoluteLink);
    }

    private void WhenNotAbsolute()
    {
        Add("not_an_absolute_url", Errors.LongUrl.NonAbsoluteLink);
    }

    private void WhenTooLong()
    {
        var tooLongUrl = "https://example.com/" + new string('a', 1005);
        Add(tooLongUrl, Errors.LongUrl.LengthTooLong);
    }
}