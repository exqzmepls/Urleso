namespace Urleso.Application.Abstractions.Services;

public interface IRandomStringGenerator
{
    public string GetString(ReadOnlySpan<char> charOptions, int length);
}