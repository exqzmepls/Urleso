namespace Urleso.SharedKernel.Results;

public sealed class Result
{
    private static readonly Result SuccessResult = new(isSuccess: true, error: null);

    private readonly Error? _error;

    private Result(bool isSuccess, Error? error)
    {
        IsSuccess = isSuccess;
        _error = error;
    }

    public Error Error => IsSuccess ? throw new NotAvailableErrorException() : _error!;

    public bool IsFailure => !IsSuccess;

    public bool IsSuccess { get; }

    public static implicit operator Result(Error error) => Failure(error);

    public static Result Success() => SuccessResult;

    private static Result Failure(Error error) => new(isSuccess: false, error);
}