using Urleso.Application.Abstractions.Data;
using Urleso.Application.Abstractions.Data.Repositories;
using Urleso.Application.Abstractions.Messaging;
using Urleso.Application.Abstractions.Services;
using Urleso.Domain.ShortenedUrls;
using Urleso.SharedKernel;

namespace Urleso.Application.ShortenedUrls.CreateShortenedUrl;

internal sealed class CreateShortenedUrlCommandHandler(
    ICodeGeneratingService codeGeneratingService,
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

        var urlCodeResult = await GenerateUrlCodeAsync(cancellationToken);
        if (urlCodeResult.IsFailure)
        {
            return urlCodeResult.Error;
        }

        var longUrl = longUrlResult.Value;
        var urlCode = urlCodeResult.Value;
        var shortenedUrl = CreateShortenedUrl(longUrl, urlCode);

        var addResult = await shortenedUrlRepository.AddAsync(shortenedUrl, cancellationToken);
        if (addResult.IsFailure)
        {
            return addResult.Error;
        }

        var saveChangesResult = await unitOfWork.SaveChangesAsync(cancellationToken);
        return saveChangesResult.IsSuccess ? shortenedUrl : saveChangesResult.Error;
    }

    private async Task<TypedResult<UrlCode>> GenerateUrlCodeAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var shortCodeResult = codeGeneratingService.GenerateUniqueCode(UrlCode.CodeDefaultLength);
            if (shortCodeResult.IsFailure)
            {
                return shortCodeResult.Error;
            }

            var shortCode = shortCodeResult.Value;
            var urlCodeResult = UrlCode.Create(shortCode);
            if (urlCodeResult.IsFailure)
            {
                return urlCodeResult.Error;
            }

            var code = urlCodeResult.Value;
            var isCodeExistResult = await shortenedUrlRepository.IsCodeExistAsync(code, cancellationToken);
            if (!isCodeExistResult.IsSuccess)
            {
                return isCodeExistResult.Error;
            }

            var isCodeExist = isCodeExistResult.Value;
            if (!isCodeExist)
            {
                return urlCodeResult;
            }
        }

        var canceledTask = Task.FromCanceled<TypedResult<UrlCode>>(cancellationToken);
        return await canceledTask;
    }

    private ShortenedUrl CreateShortenedUrl(LongUrl longUrl, UrlCode urlCode)
    {
        var id = new ShortenedUrlId(Guid.NewGuid());
        var createOnUtc = clock.GetUtcNow();
        var shortenedUrl = ShortenedUrl.Create(id, longUrl, urlCode, createOnUtc);
        return shortenedUrl;
    }
}