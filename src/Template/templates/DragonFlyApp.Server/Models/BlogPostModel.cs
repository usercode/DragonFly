// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;
using DragonFly.Generator;

namespace DragonFlyTemplate.Models;

[ContentItem]
public partial class BlogPostModel
{
    [DateField(Required = true)]
    private DateTime? _date;

    [StringField(Required = true, Index = true, ListField = true, MinLength = 8, MaxLength = 512)]
    private string? _title;

    [TextField]
    private string? _description;

    [SlugField(Required = true, Index = true)]
    private string? _slug;

    [AssetField(ListField = true, ShowPreview = true)]
    private AssetField _image;

    [BlockField]
    private BlockField _mainContent;
}
