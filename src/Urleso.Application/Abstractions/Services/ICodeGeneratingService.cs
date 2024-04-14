using Urleso.SharedKernel.Results;

namespace Urleso.Application.Abstractions.Services;

public interface ICodeGeneratingService
{
    public TypedResult<string> GenerateUniqueCode(int length);
}