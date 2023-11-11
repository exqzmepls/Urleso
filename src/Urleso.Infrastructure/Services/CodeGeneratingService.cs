using Urleso.Application.Abstractions.Services;
using Urleso.Domain.Results;

namespace Urleso.Infrastructure.Services;

internal sealed class CodeGeneratingService : ICodeGeneratingService
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

    public TypedResult<string> GenerateUniqueCode(int lenght)
    {
        var codeChars = new char[lenght];
        for (var i = 0; i < lenght; i++)
        {
            codeChars[i] = GetRandomChar();
        }

        var code = codeChars.ToString()!;
        return TypedResult<string>.Success(code);
    }

    private static char GetRandomChar()
    {
        var randomCharIndex = Random.Shared.Next(AvailableChars.Count);
        var randomChar = AvailableChars[randomCharIndex];
        return randomChar;
    }
}