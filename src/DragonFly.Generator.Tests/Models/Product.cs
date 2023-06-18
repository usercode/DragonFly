// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator;

namespace DragonFly.Tests;

[ContentItem]
public partial class Product
{
    [StringField]
    private string? _title;

    [SlugField]
    private SlugField _slug;

    [BoolField]
    private bool? _isActive;
}
