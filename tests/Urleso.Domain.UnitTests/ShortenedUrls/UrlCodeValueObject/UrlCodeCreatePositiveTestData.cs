using Xunit;

namespace Urleso.Domain.UnitTests.ShortenedUrls.UrlCodeValueObject;

internal sealed class UrlCodeCreatePositiveTestData : TheoryData<string>
{
    public UrlCodeCreatePositiveTestData()
    {
        WhenValid();
    }

    private void WhenValid()
    {
        Add("AbCd1234");
    }
}