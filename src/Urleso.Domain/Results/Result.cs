namespace Urleso.Domain.Results;

public sealed class Result : ResultBase
{
    private static readonly Result SuccessResult = new();

    private Result()
    {
    }

    private Result(Error error) : base(error)
    {
    }

    public static Result Success() => SuccessResult;

    public static Result Failure(Error error) => new(error);
}