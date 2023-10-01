namespace Urleso.Domain.Primitives;

public abstract class Entity(EntityId id) : IEquatable<Entity>
{
    public EntityId Id { get; } = id;

    public static bool operator ==(Entity? first, Entity? second) =>
        first?.Equals(second) ?? ReferenceEquals(first, second);

    public static bool operator !=(Entity? first, Entity? second) =>
        !(first == second);

    public override bool Equals(object? obj) => obj is Entity entity && Equals(entity);

    public override int GetHashCode() => Id.GetHashCode();

    public bool Equals(Entity? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        return other.Id == Id;
    }
}