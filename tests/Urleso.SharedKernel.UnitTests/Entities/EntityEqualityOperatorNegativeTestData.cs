namespace Urleso.SharedKernel.UnitTests.Entities;

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