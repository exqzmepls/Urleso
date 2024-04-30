using Urleso.Domain.ShortenedUrls;
using Urleso.SharedKernel.Results;

namespace Urleso.Domain.UnitTests.ShortenedUrls.LongUrls;

public sealed class LongUrlTests
{
    [Theory]
    [ClassData(typeof(LongUrlCreatePositiveTestData))]
    internal void Create_Should_ReturnLongUrl_WhenUrlIsValid(string url)
    {
        // Act
        var longUrl = LongUrl.Create(url).Value;

        // Assert
        longUrl.Value.ShouldBe(url);
    }

    [Theory]
    [ClassData(typeof(LongUrlCreateNegativeTestData))]
    internal void Create_Should_ReturnError_WhenUrlIsNotValid(string url, Error expectedError)
    {
        // Act
        var error = LongUrl.Create(url).Error;

        // Assert
        error.ShouldBeSameAs(expectedError);
    }
}