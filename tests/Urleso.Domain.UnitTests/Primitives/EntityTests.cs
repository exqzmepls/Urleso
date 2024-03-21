using Xunit;

namespace Urleso.Domain.UnitTests.Primitives;

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

internal sealed class EntityEqualityOperatorPositiveTestData : EntityEqualityOperatorTestData
{
    public EntityEqualityOperatorPositiveTestData()
    {
        WhenFirstNullAndSecondNull();
        WhenEntitiesHaveEqualId();
    }

    private void WhenFirstNullAndSecondNull()
    {
        DummyEntity? entity1 = null;
        DummyEntity? entity2 = null;
        Add(entity1, entity2);
    }

    private void WhenEntitiesHaveEqualId()
    {
        const string id = "3d3e0224-0c0c-4936-8e7e-e60ae2d03301";
        var entity1 = CreateEntity(id);
        var entity2 = CreateEntity(id);
        Add(entity1, entity2);
    }
}

internal sealed class EntityEqualityOperatorNegativeTestData : EntityEqualityOperatorTestData
{
    public EntityEqualityOperatorNegativeTestData()
    {
        WhenEntitiesHaveDifferentId();
        WhenFirstNullAndSecondNotNull();
        WhenFirstNotNullAndSecondNull();
    }

    private void WhenEntitiesHaveDifferentId()
    {
        var entity1 = CreateEntity("3e7aa7d5-611e-4bc3-baa6-52f051811908");
        var entity2 = CreateEntity("2c09bc83-6b15-4584-9a78-a32356e343d4");
        Add(entity1, entity2);
    }

    private void WhenFirstNullAndSecondNotNull()
    {
        DummyEntity? entity1 = null;
        var entity2 = CreateEntity("4767c60a-7a7e-4d11-980d-09e55480a94a");
        Add(entity1, entity2);
    }

    private void WhenFirstNotNullAndSecondNull()
    {
        var entity1 = CreateEntity("1848aef2-42e1-484d-a99d-14310a4a5def");
        DummyEntity? entity2 = null;
        Add(entity1, entity2);
    }
}

internal abstract class EntityEqualityOperatorTestData : TheoryData<DummyEntity?, DummyEntity?>
{
    protected static DummyEntity CreateEntity(string guidString)
    {
        var guid = Guid.Parse(guidString);
        var id = new DummyEntityId(guid);
        var entity = new DummyEntity(id);
        return entity;
    }
}