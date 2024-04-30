using Urleso.SharedKernel.Results;

namespace Urleso.SharedKernel.UnitTests.Results;

public sealed class TypedResultTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("test value string")]
    [InlineData(7)]
    internal void ImplicitSuccess_Should_ReturnSuccessWithValueAndWithoutError<T>(T value)
    {
        // Arrange
        TypedResult<T> Success() => value;

        // Act
        var result = Success();

        // Assert
        result.ShouldSatisfyAllConditions(() =>
        {
            result.IsSuccess.ShouldBeTrue();
            result.IsFailure.ShouldBeFalse();
            result.Value.ShouldBe(value);
            Should.Throw<NotAvailableErrorException>(() => result.Error);
        });
    }

    [Theory]
    [InlineData(null)]
    [InlineData("test value string")]
    [InlineData(7)]
    internal void ImplicitFailure_Should_ReturnFailureWithErrorAndWithoutValue<T>(T value)
    {
        // Arrange
        var error = new Error(code: "Tests.TypedResult", message: "Typed result tests error message");
        TypedResult<T> Failure() => error;

        // Act
        var result = Failure();

        // Assert
        result.ShouldSatisfyAllConditions(() =>
        {
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.ShouldBe(error);
            Should.Throw<NotAvailableValueException>(() => result.Value);
        });
    }
}