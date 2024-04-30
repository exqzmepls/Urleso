using Urleso.Domain.ShortenedUrls;

namespace Urleso.Domain.UnitTests.ShortenedUrls;

public sealed class ShortenedUrlTests
{
    [Fact]
    internal void Create_Should_ReturnCorrectlySetProperties()
    {
        // Arrange
        var id = new ShortenedUrlId(Guid.Parse("126596ba-568c-4e23-ba7c-890e4d344bf3"));
        var longUrl = LongUrl.Create("https://example.com").Value;
        var code = UrlCode.Create("AbCd1234").Value;
        var createdOnUtc = DateTime.Parse("2023-10-10T17:09:41+0000");

        // Act
        var shortenedUrl = ShortenedUrl.Create(id, longUrl, code, createdOnUtc);

        // Assert
        shortenedUrl.ShouldSatisfyAllConditions(() =>
        {
            shortenedUrl.Id.ShouldBe(id);
            shortenedUrl.LongUrl.ShouldBe(longUrl);
            shortenedUrl.Code.ShouldBe(code);
            shortenedUrl.CreatedOnUtc.ShouldBe(createdOnUtc);
        });
    }
}