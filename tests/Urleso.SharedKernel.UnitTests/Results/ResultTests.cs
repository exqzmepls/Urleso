using Urleso.SharedKernel.Results;

namespace Urleso.SharedKernel.UnitTests.Results;

public sealed class ResultTests
{
    [Fact]
    internal void ImplicitSuccess_Should_ReturnSuccessWithoutError()
    {
        // Act
        var result = Result.Success();

        // Assert
        result.ShouldSatisfyAllConditions(() =>
        {
            result.IsSuccess.ShouldBeTrue();
            result.IsFailure.ShouldBeFalse();
            Should.Throw<NotAvailableErrorException>(() => result.Error);
        });
    }

    [Fact]
    internal void ImplicitFailure_Should_ReturnFailureWithError()
    {
        // Arrange
        var error = new Error(code: "Tests.Result", message: "Result tests error message");
        Result Failure() => error;

        // Act
        var result = Failure();

        // Assert
        result.ShouldSatisfyAllConditions(() =>
        {
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.ShouldBe(error);
        });
    }
}