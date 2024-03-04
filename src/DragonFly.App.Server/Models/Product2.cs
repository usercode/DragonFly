// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;
using DragonFly.BlockField;
using DragonFly.Generator;
using DragonFlyABC;

namespace DragonFlyTEST;

[ContentItem(Preview = "http://localhost/preview/{{Title}}")]
public partial class Product2
{
    [StringField(Required = true, Index = true, MaxLength = 1024, ReferenceField = true, ListField = true, QueryField = true)]
    private string? _title;

    [SlugField(Index = true, QueryField = true)]
    private SlugField _slug;

    [BoolField(Index = true, QueryField = true)]
    private bool? _isActive;

    [IntegerField(Index = true, QueryField = true)]
    private long? _quantity;

    [FloatField(Index = true, QueryField = true)]
    private double? _quantity2;

    [HtmlField]
    private BlockField _htmlContent;

    [BlockField]
    private BlockField _mainContent;

    [AssetField(ShowPreview = true, Index = true, QueryField = true)]
    private AssetField _image;

    [AssetField(ShowPreview = true, Index = true, QueryField = true)]
    private Asset _image2;

    [ReferenceField(Index = true, QueryField = true)]
    private ReferenceField _customerA;

    [ReferenceField]
    private ContentItem? _customerB;

    [ReferenceField]
    private Customer? _customerC;

    [GeolocationField(Index = true, QueryField = true)]
    private GeolocationField _location;

    [TextField]
    private TextField _text;

    [UrlField]
    private UrlField _url;
}
