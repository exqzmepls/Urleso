using Urleso.Domain.ShortenedUrls;
using Urleso.SharedKernel.Results;
using Xunit;

namespace Urleso.Domain.UnitTests.ShortenedUrls.UrlCodeValueObject;

internal sealed class UrlCodeCreateNegativeTestData : TheoryData<string, Error>
{
    public UrlCodeCreateNegativeTestData()
    {
        WhenEmptyString();
        WhenShort();
        WhenLong();
        WhenInvalid();
    }

    private void WhenEmptyString()
    {
        Add(string.Empty, Errors.UrlCode.WrongLength);
    }

    private void WhenShort()
    {
        Add("Short00", Errors.UrlCode.WrongLength);
    }

    private void WhenLong()
    {
        Add("LongCode0", Errors.UrlCode.WrongLength);
    }

    private void WhenInvalid()
    {
        Add("In_valid", Errors.UrlCode.InvalidChars);
    }
}