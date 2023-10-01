namespace Urleso.Domain.Primitives;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public override bool Equals(object? obj) => obj is ValueObject other && ValuesAreEqual(other);

    public override int GetHashCode() => GetValues().Aggregate(default(int), HashCode.Combine);

    public bool Equals(ValueObject? other) => other is not null && ValuesAreEqual(other);

    protected abstract IEnumerable<object> GetValues();

    private bool ValuesAreEqual(ValueObject other) => GetValues().SequenceEqual(other.GetValues());
}