using Urleso.Domain.ShortenedUrls;
using Urleso.SharedKernel;
using Xunit;

namespace Urleso.Domain.UnitTests.ShortenedUrls.LongUrlValueObject;

public sealed class LongUrlTests
{
    [Theory]
    [ClassData(typeof(LongUrlCreatePositiveTestData))]
    internal void Create_Should_ReturnSuccessResult(string url)
    {
        var result = LongUrl.Create(url);

        Assert.True(result.IsSuccess);
        var longUrl = result.Value;
        Assert.Equal(url, longUrl.Value);
    }

    [Theory]
    [ClassData(typeof(LongUrlCreateNegativeTestData))]
    internal void Create_Should_ReturnFailureResult(string url, Error expectedError)
    {
        var result = LongUrl.Create(url);

        Assert.False(result.IsSuccess);
        Assert.Same(expectedError, result.Error);
    }
}