// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
