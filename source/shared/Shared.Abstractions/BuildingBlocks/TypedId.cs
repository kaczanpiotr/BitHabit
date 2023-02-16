namespace Shared.Abstractions.BuildingBlocks;

public abstract class TypedId : IEquatable<TypedId>
{
    public Guid Value { get; }

    protected TypedId(Guid value)
    {
        Value = value;
    }

    public bool IsEmpty() => Value == Guid.Empty;

    public static implicit operator Guid(TypedId value) => value.Value;

    public bool Equals(TypedId other)
    {
        if (other is null)
            return false;

        return ReferenceEquals(this, other) || Value.Equals(other.Value);
    }

    public override bool Equals(object obj)
    {
        if (obj is null)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        return obj is TypedId other && Equals(other);
    }

    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value.ToString();
}