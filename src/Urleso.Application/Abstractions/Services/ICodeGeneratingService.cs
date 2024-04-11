using Urleso.SharedKernel;

namespace Urleso.Application.Abstractions.Services;

public interface ICodeGeneratingService
{
    public TypedResult<string> GenerateUniqueCode(int length);
}