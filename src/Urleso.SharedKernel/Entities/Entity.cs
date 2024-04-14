namespace Urleso.SharedKernel.Entities;

public abstract class Entity<TEntityId>(TEntityId id) : IEquatable<Entity<TEntityId>>
    where TEntityId : IEquatable<TEntityId>
{
    public TEntityId Id => id;

    public static bool operator !=(Entity<TEntityId>? first, Entity<TEntityId>? second) => !(first == second);

    public static bool operator ==(Entity<TEntityId>? first, Entity<TEntityId>? second) =>
        first?.Equals(second) ?? ReferenceEquals(first, second);

    public override int GetHashCode() => Id.GetHashCode();

    public override bool Equals(object? obj) => Equals(obj as Entity<TEntityId>);

    public bool Equals(Entity<TEntityId>? other)
    {
        if (other is null)
        {
            return false;
        }

        return GetType() == other.GetType() && Id.Equals(other.Id);
    }
}