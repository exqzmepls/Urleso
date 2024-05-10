using Urleso.Application.Abstractions.Data;
using Urleso.Application.Abstractions.Data.Repositories;
using Urleso.Application.Abstractions.Messaging;
using Urleso.Application.Abstractions.Services;
using Urleso.Domain.ShortenedUrls;
using Urleso.SharedKernel.Results;

namespace Urleso.Application.ShortenedUrls.CreateShortenedUrl;

internal sealed class CreateShortenedUrlCommandHandler(
    IRandomStringGenerator randomStringGenerator,
    IShortenedUrlRepository shortenedUrlRepository,
    IClock clock,
    IUnitOfWork unitOfWork
)
    : ICommandHandler<CreateShortenedUrlCommand, ShortenedUrl>
{
    public async Task<TypedResult<ShortenedUrl>> Handle(CreateShortenedUrlCommand command,
        CancellationToken cancellationToken)
    {
        var longUrlResult = LongUrl.Create(command.LongUrl);
        if (longUrlResult.IsFailure)
        {
            return longUrlResult.Error;
        }

        var urlCodeResult = CreateUrlCode();
        if (urlCodeResult.IsFailure)
        {
            return urlCodeResult.Error;
        }

        var shortenedUrl = CreateShortenedUrl(longUrlResult.Value, urlCodeResult.Value);

        await shortenedUrlRepository.AddAsync(shortenedUrl, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return shortenedUrl;
    }

    private TypedResult<UrlCode> CreateUrlCode()
    {
        var urlUniqueCode = randomStringGenerator.GetString(UrlCode.CodeAlphabet, UrlCode.CodeLength);
        var urlCodeResult = UrlCode.Create(urlUniqueCode);
        return urlCodeResult;
    }

    private ShortenedUrl CreateShortenedUrl(LongUrl longUrl, UrlCode urlCode)
    {
        var id = new ShortenedUrlId(Guid.NewGuid());
        var createOnUtc = clock.GetUtcNow();
        var shortenedUrl = ShortenedUrl.Create(id, longUrl, urlCode, createOnUtc);
        return shortenedUrl;
    }
}