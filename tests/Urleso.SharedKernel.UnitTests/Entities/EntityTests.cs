using Urleso.SharedKernel.Entities;

namespace Urleso.SharedKernel.UnitTests.Entities;

public sealed class EntityTests
{
    [Fact]
    internal void InequalityOperator_Should_ReturnFalse_WhenBothEntitiesAreNull()
    {
        // Arrange
        Entity<Guid>? entity1 = null;
        Entity<Guid>? entity2 = null;

        // Act
        var result = entity1 != entity2;

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    internal void EqualityOperator_Should_ReturnTrue_WhenEntitiesHaveEqualId()
    {
        // Arrange
        var id = ParseId("3d3e0224-0c0c-4936-8e7e-e60ae2d03301");
        var entity1 = CreateEntity(id);
        var entity2 = CreateEntity(id);

        // Act
        var result = entity1 == entity2;

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    internal void Equals_Should_ReturnFalse_WhenTypeOfOtherIsNotEqualWithEntityType()
    {
        // Arrange
        var entity1 = CreateEntity("1848aef2-42e1-484d-a99d-14310a4a5def");
        var entity2 = new object();

        // Act
        var result = entity1.Equals(entity2);

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    internal void Equals_Should_ReturnFalse_WhenSecondEntityIsNull()
    {
        // Arrange
        var entity1 = CreateEntity("2c09bc83-6b15-4584-9a78-a32356e343d4");
        Entity<Guid>? entity2 = null;

        // Act
        var result = entity1.Equals(entity2);

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    internal void GetHashCode_Should_ReturnIdHashCode()
    {
        // Arrange
        var id = ParseId("0b1f4a3f-4b3f-4b3f-9b3f-9b3f9b3f9b3f");
        var entity = CreateEntity(id);
        var idHashCode = id.GetHashCode();

        // Act
        var entityHashCode = entity.GetHashCode();

        // Assert
        entityHashCode.ShouldBe(idHashCode);
    }

    private static Entity<Guid> CreateEntity(string id) => new DummyEntity(ParseId(id));

    private static Entity<Guid> CreateEntity(Guid id) => new DummyEntity(id);

    private static Guid ParseId(string stringId) => Guid.Parse(stringId);
}