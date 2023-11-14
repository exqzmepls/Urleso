using Urleso.Application.Abstractions.Messaging;
using Urleso.Domain.ShortenedUrls;

namespace Urleso.Application.ShortenedUrls.CreateShortenedUrl;

public sealed class CreateShortenedUrlCommand : ICommand<ShortenedUrl>
{
    public required string LongUrl { get; init; }
}