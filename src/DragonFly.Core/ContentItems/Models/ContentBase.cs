// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public abstract class ContentBase<T> : Entity<T>
    where T : IEntity
{
    public virtual DateTime? CreatedAt { get; set; }

    public virtual DateTime? ModifiedAt { get; set; }

    public virtual DateTime? PublishedAt { get; set; }

    public virtual int Version { get; set; }

}
