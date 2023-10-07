namespace Urleso.Domain.Primitives;

public abstract class Entity<TEntityId>(TEntityId id) : IEquatable<Entity<TEntityId>>
    where TEntityId : EntityId
{
    public TEntityId Id { get; } = id;

    public static bool operator ==(Entity<TEntityId>? first, Entity<TEntityId>? second) =>
        first?.Equals(second) ?? ReferenceEquals(first, second);

    public static bool operator !=(Entity<TEntityId>? first, Entity<TEntityId>? second) =>
        !(first == second);

    public override bool Equals(object? obj) => obj is Entity<TEntityId> entity && Equals(entity);

    public override int GetHashCode() => Id.GetHashCode();

    public bool Equals(Entity<TEntityId>? other)
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