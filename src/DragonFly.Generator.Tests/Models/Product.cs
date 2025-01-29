// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator;

namespace DragonFly.Tests;

[ContentItem]
public partial class Product
{
    [StringField(Required = true, MaxLength = 1024)]
    public partial string? Title { get; set; }

    [SlugField]
    public partial SlugField Slug { get;set; }
}
