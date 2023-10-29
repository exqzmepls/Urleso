using Urleso.Domain.Results;

namespace Urleso.Application.Abstractions.Services;

public interface ICodeGeneratingService
{
    public TypedResult<string> GenerateUniqueCode(int lenght);
}