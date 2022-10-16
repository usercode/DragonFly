// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public abstract class ContentBase : Entity
{
    public virtual DateTime? CreatedAt { get; set; }

    public virtual DateTime? ModifiedAt { get; set; }

    public virtual DateTime? PublishedAt { get; set; }

    public virtual int Version { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is ContentBase other)
        {
            return Id == other.Id;
        }

        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return Id.ToString();
    }
}
