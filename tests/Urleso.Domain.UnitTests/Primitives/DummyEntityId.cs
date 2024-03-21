using Urleso.Domain.Primitives;

namespace Urleso.Domain.UnitTests.Primitives;

internal sealed record DummyEntityId(Guid Value) : EntityId(Value);