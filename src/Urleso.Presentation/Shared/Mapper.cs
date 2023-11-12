using Urleso.Domain.Results;

namespace Urleso.Presentation.Shared;

internal static class Mapper
{
    public static ErrorDetails MapToErrorDetails(this Error error)
    {
        var errorDetails = new ErrorDetails
        {
            Code = error.Code,
            Description = error.Message
        };
        return errorDetails;
    }
}