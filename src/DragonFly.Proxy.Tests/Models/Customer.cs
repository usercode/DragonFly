// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

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

    public string Remark { get; set; }
}
