using Xunit;
using Urleso.Domain.ShortenedUrls;
using Urleso.SharedKernel.Results;

namespace Urleso.Domain.UnitTests.ShortenedUrls.UrlCodeValueObject;

public sealed class UrlCodeTests
{
    [Theory]
    [ClassData(typeof(UrlCodeCreatePositiveTestData))]
    internal void Create_ValidUrlCode_ReturnsSuccessResult(string code)
    {
        var result = UrlCode.Create(code);

        Assert.True(result.IsSuccess);
        var urlCode = result.Value;
        Assert.Equal(code, urlCode.Value);
    }

    [Theory]
    [ClassData(typeof(UrlCodeCreateNegativeTestData))]
    public void Create_Should_ReturnFailureResult(string code, Error expectedError)
    {
        var result = UrlCode.Create(code);

        Assert.False(result.IsSuccess);
        Assert.Same(expectedError, result.Error);
    }
}