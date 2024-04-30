namespace Urleso.Domain.UnitTests.ShortenedUrls.UrlCodes;

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