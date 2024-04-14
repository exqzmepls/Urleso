namespace Urleso.SharedKernel.Results;

public sealed class NotAvailableValueException() : Exception(DetailsMessage)
{
    internal const string DetailsMessage = "The value of a failure result can not be accessed.";
}