// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator;

namespace DragonFly.Tests;

[ContentItem]
public partial class Customer
{
    [StringField]
    public partial string? Firstname { get; set; }

    [StringField]
    public partial string? Lastname { get; set; }

    [StringField]
    public partial string? Street { get;set; }

    [SlugField]
    public partial SlugField Slug { get; set; }

    [StringField]
    public partial string? Remark { get; set; }
}
