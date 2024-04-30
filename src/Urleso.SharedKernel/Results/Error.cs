namespace Urleso.SharedKernel.Results;

public sealed class Error
{
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; }

    public string Message { get; }
}