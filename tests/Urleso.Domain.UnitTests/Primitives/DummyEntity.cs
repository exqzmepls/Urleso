using Urleso.Domain.Primitives;

namespace Urleso.Domain.UnitTests.Primitives;

internal sealed class DummyEntity(DummyEntityId id) : Entity<DummyEntityId>(id);