// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;
using DragonFly.BlockField;
using DragonFly.Generator;
using DragonFlyABC;

namespace DragonFlyTEST;

[ContentItem(Preview = "http://localhost/preview/{{Title}}")]
public partial class Product
{
    [StringField(Required = true, MaxLength = 1024, ReferenceField = true, ListField = true, QueryField = true)]
    private string? _title;

    [SlugField(Index = true)]
    private SlugField _slug;

    [BoolField(Index = true, QueryField = true)]
    private bool? _isActive;

    [BlockField]
    private BlockField _mainContent;

    [AssetField(ShowPreview = true)]
    private AssetField _image;

    [AssetField(ShowPreview = true)]
    private Asset _image2;

    [ReferenceField]
    private ReferenceField _customerA;

    [ReferenceField]
    private ContentItem? _customerB;

    [ReferenceField]
    private Customer? _customerC;

    [GeolocationField(Index = true, QueryField = true)]
    private GeolocationField _location;
}
