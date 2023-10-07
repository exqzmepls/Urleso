namespace Urleso.Domain.Shared;

public abstract class ResultBase
{
    private readonly Error? _error;

    protected ResultBase() : this(true, null)
    {
    }

    protected ResultBase(Error error) : this(false, error)
    {
    }

    private ResultBase(bool isSuccess, Error? error)
    {
        IsSuccess = isSuccess;
        _error = error;
    }

    public bool IsSuccess { get; }

    public Error Error => !IsSuccess
        ? _error!
        : throw new InvalidOperationException("The error of a success result can not be accessed.");
}