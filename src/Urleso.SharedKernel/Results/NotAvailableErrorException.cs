namespace Urleso.SharedKernel.Results;

public sealed class NotAvailableErrorException() : Exception(DetailsMessage)
{
    internal const string DetailsMessage = "The error of a success result can not be accessed.";
}