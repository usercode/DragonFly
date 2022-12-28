// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;
using DragonFly.BlockField;
using DragonFly.Proxy.Attributes;

namespace DragonFlyTemplate.Models;

[ContentItem("BlogPost")]
public class BlogPostModel : EntityPageModel
{
    [DateField(Required = true)]
    public virtual DateTime? Date { get; set; }

    [StringField(Required = true, Searchable = true, ListField = true, MinLength = 8, MaxLength = 512)]
    public virtual string Title { get; set; }

    [TextField]
    public virtual string Description { get; set; }

    [SlugField(Required = true, Index = true)]
    public virtual string Slug { get; set; }

    [AssetField(ListField = true, ShowPreview = true)]
    public virtual AssetField Image { get; set; }

    [BlockField]
    public virtual BlockField MainContent { get; set; }
}
