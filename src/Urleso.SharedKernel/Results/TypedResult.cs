namespace Urleso.SharedKernel.Results;

public sealed class TypedResult<TValue>
{
    private readonly Result _result;
    private readonly TValue? _value;

    private TypedResult(Result result, TValue? value)
    {
        _result = result;
        _value = value;
    }

    public TValue Value => IsSuccess ? _value! : throw new NotAvailableValueException();

    public Error Error => _result.Error;

    public bool IsFailure => _result.IsFailure;

    public bool IsSuccess => _result.IsSuccess;

    public static implicit operator TypedResult<TValue>(TValue value) => Success(value);

    public static implicit operator TypedResult<TValue>(Error error) => Failure(error);

    private static TypedResult<TValue> Success(TValue value)
    {
        var result = Result.Success();
        var typedResult = new TypedResult<TValue>(result, value);
        return typedResult;
    }

    private static TypedResult<TValue> Failure(Error error)
    {
        var typedResult = new TypedResult<TValue>(error, value: default);
        return typedResult;
    }
}