namespace Urleso.SharedKernel.UnitTests.Entities;

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