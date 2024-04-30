using Urleso.Domain.ShortenedUrls;
using Urleso.SharedKernel.Results;

namespace Urleso.Domain.UnitTests.ShortenedUrls.UrlCodes;

public sealed class UrlCodeTests
{
    [Theory]
    [ClassData(typeof(UrlCodeCreatePositiveTestData))]
    internal void Create_Should_ReturnUrlCode_WhenCodeIsValid(string code)
    {
        // Act
        var urlCode = UrlCode.Create(code).Value;

        // Assert
        urlCode.Value.ShouldBe(code);
    }

    [Theory]
    [ClassData(typeof(UrlCodeCreateNegativeTestData))]
    public void Create_Should_ReturnError_WhenCodeIsNotValid(string code, Error expectedError)
    {
        // Act
        var error = UrlCode.Create(code).Error;

        // Assert
        error.ShouldBeSameAs(expectedError);
    }
}