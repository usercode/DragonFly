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
    public partial DateTime? Date { get; set; }

    [StringField(Required = true, Index = true, ListField = true, MinLength = 8, MaxLength = 512)]
    public partial string? Title { get; set; }

    [TextField]
    public partial string? Description { get; set; }

    [SlugField(Required = true, Index = true)]
    public partial string? Slug { get; set; }

    [AssetField(ListField = true, ShowPreview = true)]
    public partial AssetField Image { get; set; }

    [BlockField]
    public partial BlockField MainContent { get; set; }
}
