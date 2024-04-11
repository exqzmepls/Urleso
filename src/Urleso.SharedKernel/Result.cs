namespace Urleso.SharedKernel;

public sealed class Result
{
    private static readonly Result SuccessResult = new(isSuccess: true, error: null);

    private readonly Error? _error;

    private Result(bool isSuccess, Error? error)
    {
        IsSuccess = isSuccess;
        _error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error => IsSuccess
        ? throw new InvalidOperationException("The error of a success result can not be accessed.")
        : _error!;

    public static implicit operator Result(Error error) => Failure(error);

    public static Result Success() => SuccessResult;

    public static Result Failure(Error error) => new(isSuccess: false, error);
}