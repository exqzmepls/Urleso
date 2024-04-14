using Urleso.SharedKernel.Entities;

namespace Urleso.SharedKernel.UnitTests.Entities;

internal sealed class DummyEntity(Guid id) : Entity<Guid>(id);