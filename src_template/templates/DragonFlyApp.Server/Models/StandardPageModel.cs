// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;
using DragonFly.Generator;

namespace DragonFlyTemplate.Models;

[ContentItem]
public partial class StandardPageModel
{
    [StringField(Required = true, ListField = true)]
    public partial string? Title { get; set; }

    [SlugField(Required = true, Index = true)]
    public partial string? Slug { get; set; }

    [BoolField(Required = true, Index = true)]
    public partial bool? NoFollow { get; set; }

    [BoolField(Required = true, Index = true)]
    public partial bool? NoIndex { get; set; }

    [BoolField(Required = true, Index = true)]
    public partial bool? IsStartPage { get; set; }

    [BoolField(Required = true, Index = true)]
    public partial bool? IsFooterPage { get; set; }

    [BlockField]
    public partial BlockField MainContent { get; set; }
}
