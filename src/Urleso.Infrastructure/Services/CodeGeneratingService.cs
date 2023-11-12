using Microsoft.Extensions.Logging;
using Urleso.Application.Abstractions.Services;
using Urleso.Domain.Results;

namespace Urleso.Infrastructure.Services;

internal sealed class CodeGeneratingService(
        ILogger<CodeGeneratingService> logger
    )
    : ICodeGeneratingService
{
    private static readonly IReadOnlyList<char> AvailableChars;

    static CodeGeneratingService()
    {
        var availableChars = new List<char>();
        AddChars(availableChars, 'A', 'Z');
        AddChars(availableChars, 'a', 'z');
        AddChars(availableChars, '0', '9');

        AvailableChars = availableChars;
        return;

        void AddChars(List<char> source, char start, char end)
        {
            var count = end - start + 1;
            var chars = Enumerable.Range(start, count).Select(Convert.ToChar);
            source.AddRange(chars);
        }
    }

    public TypedResult<string> GenerateUniqueCode(int length)
    {
        logger.LogInformation("Generation unique code with length of '{UniqueCodeLength}'...", length);
        var uniqueCode = GenerateUniqueCodeInternal(length);
        logger.LogInformation("Generated unique code: {UniqueCode}", uniqueCode);
        return TypedResult<string>.Success(uniqueCode);
    }

    private string GenerateUniqueCodeInternal(int length)
    {
        var codeChars = new char[length];
        for (var index = 0; index < length; index++)
        {
            logger.LogDebug("Getting char for index '{UniqueCodeCharIndex}'...", index);
            var uniqueCodeChar = GetRandomChar();
            codeChars[index] = uniqueCodeChar;
            logger.LogDebug("'{UniqueCodeCharIndex}' char: {UniqueCodeChar}", index, uniqueCodeChar);
        }

        return codeChars.ToString()!;
    }

    private char GetRandomChar()
    {
        logger.LogTrace("Getting random char...");
        var randomCharIndex = Random.Shared.Next(AvailableChars.Count);
        logger.LogTrace("Random char index: {RandomCharIndex}", randomCharIndex);
        var randomChar = AvailableChars[randomCharIndex];
        logger.LogTrace("Random char: {RandomChar}", randomChar);
        return randomChar;
    }
}