namespace Urleso.SharedKernel.UnitTests;

public sealed class ErrorTests
{
    [Fact]
    public void Constructor_Should_SetCodeAndMessage()
    {
        // Arrange
        const string code = "Tests.Error";
        const string message = "Error tests error message";

        // Act
        var error = new Error(code, message);

        // Assert
        error.ShouldSatisfyAllConditions(() =>
        {
            error.Code.ShouldBe(code);
            error.Message.ShouldBe(message);
        });
    }
}