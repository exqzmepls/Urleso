namespace Urleso.SharedKernel;

public sealed class TypedResult<TValue>
{
    private readonly Result _result;
    private readonly TValue? _value;

    private TypedResult(Result result, TValue? value)
    {
        _result = result;
        _value = value;
    }

    public bool IsSuccess => _result.IsSuccess;

    public bool IsFailure => _result.IsFailure;

    public Error Error => _result.Error;

    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public static implicit operator TypedResult<TValue>(TValue value) => Success(value);

    public static implicit operator TypedResult<TValue>(Error error) => Failure(error);

    public static TypedResult<TValue> Success(TValue value)
    {
        var result = Result.Success();
        var typedResult = new TypedResult<TValue>(result, value);
        return typedResult;
    }

    public static TypedResult<TValue> Failure(Error error)
    {
        var result = Result.Failure(error);
        var typedResult = new TypedResult<TValue>(result, value: default);
        return typedResult;
    }
}