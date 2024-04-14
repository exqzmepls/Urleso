namespace Urleso.SharedKernel.UnitTests.Entities;

internal abstract class EntityEqualityOperatorTestData : TheoryData<DummyEntity?, DummyEntity?>
{
    protected static DummyEntity CreateEntity(string guidString)
    {
        var id = Guid.Parse(guidString);
        var entity = new DummyEntity(id);
        return entity;
    }
}