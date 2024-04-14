using Urleso.SharedKernel.Results;

namespace Urleso.SharedKernel.UnitTests.Results;

public sealed class ExceptionsTests
{
    [Fact]
    internal void NotAvailableValueException_Should_HaveCorrectMessage()
    {
        // Act
        var exception = new NotAvailableValueException();

        // Assert
        exception.Message.ShouldBe(NotAvailableValueException.DetailsMessage);
    }

    [Fact]
    internal void NotAvailableErrorException_Should_HaveCorrectMessage()
    {
        // Act
        var exception = new NotAvailableErrorException();

        // Assert
        exception.Message.ShouldBe(NotAvailableErrorException.DetailsMessage);
    }
}