using System.ComponentModel.DataAnnotations;

namespace Urleso.Presentation.Api.ShortenedUrls.CreateShortenedUrl;

internal sealed class RedirectSettings
{
    public const string ConfigurationSection = "Redirect";

    [Required]
    [Url]
    public required string BaseAddress { get; init; } // must have "/" in the end
}