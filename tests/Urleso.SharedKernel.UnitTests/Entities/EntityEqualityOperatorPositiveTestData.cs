namespace Urleso.SharedKernel.UnitTests.Entities;

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