// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator;

namespace DragonFly;

[ContentItem]
public partial class Product
{
    [StringField(Required = true, MaxLength = 1024)]
    private string? _title;

    [SlugField]
    private SlugField _slug;

    [BoolField]
    private bool? _isActive;

    [BlockField]
    private BlockField.BlockField _mainContent;
}
