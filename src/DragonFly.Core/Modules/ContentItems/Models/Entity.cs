// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public abstract class Entity<T> : IEntity, IEquatable<T>
    where T : IEntity
{
    protected Guid _id;

    /// <summary>
    /// Id
    /// </summary>
    public virtual Guid Id { get => _id; set => _id = value; }

    public virtual bool IsNew() => Id == Guid.Empty;

    public override bool Equals(object? obj)
    {
        if (obj is T other)
        {
            return Equals(other);
        }

        return false;
    }

    public virtual bool Equals(T? other)
    {
        if (other is null)
        {
            return false;
        }

        return _id == other.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(typeof(T), Id);
    }

    public override string ToString()
    {
        return Id.ToString();
    }
}
