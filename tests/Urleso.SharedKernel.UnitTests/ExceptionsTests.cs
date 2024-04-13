namespace Urleso.SharedKernel.UnitTests;

public sealed class ExceptionsTests
{
    [Fact]
    public void NotAvailableValueException_Should_HaveCorrectMessage()
    {
        // Act
        var exception = new NotAvailableValueException();

        // Assert
        exception.Message.ShouldBe(NotAvailableValueException.DetailsMessage);
    }

    [Fact]
    public void NotAvailableErrorException_Should_HaveCorrectMessage()
    {
        // Act
        var exception = new NotAvailableErrorException();

        // Assert
        exception.Message.ShouldBe(NotAvailableErrorException.DetailsMessage);
    }
}