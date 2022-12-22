// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Proxy.Tests.Models;

public class Customer : IContentModel
{
    public Customer()
    {
    }

    public virtual Guid Id { get; set; }

    public virtual string? Firstname { get; set; }

    public virtual string? Lastname { get; set; }

    public virtual bool IsActive { get; set; }

    public virtual long Value { get; set; }

    public virtual string? Street { get; set; }

    public virtual SlugField Slug { get; set; }
}
