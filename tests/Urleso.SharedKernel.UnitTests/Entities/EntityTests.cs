namespace Urleso.SharedKernel.UnitTests.Entities;

public sealed class EntityTests
{
    [Theory]
    [ClassData(typeof(EntityEqualityOperatorPositiveTestData))]
    internal void EqualityOperator_Should_ReturnTrue(DummyEntity? entity1, DummyEntity? entity2)
    {
        var areEqual = entity1 == entity2;

        Assert.True(areEqual);
    }

    [Theory]
    [ClassData(typeof(EntityEqualityOperatorNegativeTestData))]
    internal void EqualityOperator_Should_ReturnFalse(DummyEntity? entity1, DummyEntity? entity2)
    {
        var areEqual = entity1 == entity2;

        Assert.False(areEqual);
    }
}