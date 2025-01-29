// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;
using DragonFly.Generator;
using DragonFlyABC;

namespace DragonFlyTEST;

[ContentItem(Preview = "http://localhost/preview/{{Title}}")]
public partial class Product2
{
    [StringField(Required = true, Index = true, MaxLength = 1024, ReferenceField = true, ListField = true, QueryField = true)]
    public partial string? Title { get; set; }

    [SlugField(Index = true, QueryField = true, TargetField = nameof(Title))]
    public partial SlugField Slug { get; set; }

    [BoolField(Index = true, QueryField = true)]
    public partial bool? IsActive { get; set; }

    [IntegerField(Index = true, QueryField = true)]
    public partial long? Quantity { get; set; }

    [FloatField]
    public partial double? Quantity2 { get; set; }

    [BlockField]
    public partial BlockField MainContent { get; set; }

    [AssetField(ShowPreview = true, Index = true, QueryField = true)]
    public partial AssetField Image { get; set; }

    [AssetField(ShowPreview = true)]
    public partial Asset Image2 { get; set; }

    [ReferenceField]
    public partial ReferenceField CustomerA { get; set; }

    [ReferenceField]
    public partial ContentItem? CustomerB { get; set; }

    [ReferenceField]
    public partial Customer? CustomerC { get; set; }

    [GeolocationField]
    public partial GeolocationField Location { get; set; }

    [TextField]
    public partial TextField Text { get; set; }

    [UrlField]
    public partial UrlField Url { get; set; }
}
