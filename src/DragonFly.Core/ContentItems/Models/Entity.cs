// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public abstract class Entity
{
    protected Guid _id;

    /// <summary>
    /// Id
    /// </summary>
    public virtual Guid Id { get => _id; set => _id = value; }

    public virtual bool IsNew() => Id == Guid.Empty;
}
