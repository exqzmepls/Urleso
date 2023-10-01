namespace Urleso.Domain.Shared;

public sealed class Error
{
    internal Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; }

    public string Message { get; }
}