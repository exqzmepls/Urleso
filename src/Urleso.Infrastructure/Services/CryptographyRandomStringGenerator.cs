using System.Security.Cryptography;
using Microsoft.Extensions.Logging;
using Urleso.Application.Abstractions.Services;

namespace Urleso.Infrastructure.Services;

internal sealed class CryptographyRandomStringGenerator(
    ILogger<CryptographyRandomStringGenerator> logger
)
    : IRandomStringGenerator
{
    public string GetString(ReadOnlySpan<char> charOptions, int length)
    {
        logger.LogInformation("Generating {UniqueStringLength} chars unique string...", length);
        var uniqueString = RandomNumberGenerator.GetString(charOptions, length);
        logger.LogInformation("Generated {UniqueString}", uniqueString);
        return uniqueString;
    }
}