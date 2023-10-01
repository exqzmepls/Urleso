namespace Urleso.Domain.Primitives;

public readonly struct EntityId : IEquatable<EntityId>
{
    private readonly Guid _value;

    private EntityId(Guid value)
    {
        _value = value;
    }

    public static EntityId New()
    {
        var value = Guid.NewGuid();
        return new EntityId(value);
    }

    public static bool operator ==(EntityId first, EntityId second) => first.Equals(second);
    public static bool operator !=(EntityId first, EntityId second) => !(first == second);

    public override string ToString() => _value.ToString();
    public override int GetHashCode() => _value.GetHashCode();
    public override bool Equals(object? obj) => obj is EntityId other && Equals(other);

    public bool Equals(EntityId other) => _value.Equals(other._value);
}