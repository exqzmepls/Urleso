namespace Urleso.Domain.Results;

public sealed class TypedResult<TValue> : ResultBase
{
    private readonly TValue? _value;

    private TypedResult(TValue value)
    {
        _value = value;
    }

    private TypedResult(Error error) : base(error)
    {
    }

    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public static TypedResult<TValue> Success(TValue value) => new(value);

    public static TypedResult<TValue> Failure(Error error) => new(error);
}