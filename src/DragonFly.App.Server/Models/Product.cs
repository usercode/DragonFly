// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;
using DragonFly.BlockField;
using DragonFly.Generator;

namespace DragonFlyTEST;

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
    private BlockField _mainContent;

    [AssetField(ShowPreview = true)]
    private AssetField _image;

    [ReferenceField]
    private ReferenceField _reference;
}
