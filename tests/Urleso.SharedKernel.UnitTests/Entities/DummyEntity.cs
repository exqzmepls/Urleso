using Urleso.SharedKernel.Entities;

namespace Urleso.SharedKernel.UnitTests.Entities;

internal sealed class DummyEntity(DummyEntityId id) : Entity<DummyEntityId>(id);