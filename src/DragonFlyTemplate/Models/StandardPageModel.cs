// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;
using DragonFly.Proxy.Attributes;
using DragonFly.BlockField;

namespace DragonFlyTemplate.Models;

[ContentItem("StandardPage")]
public class StandardPageModel : EntityPageModel
{
    [StringField(Required = true, ListField = true)]
    public virtual string Title { get; set; }

    [SlugField(Required = true, Index = true)]
    public virtual string Slug { get; set; }

    [BoolField(Required = true, Index = true)]
    public virtual bool NoFollow { get; set; }

    [BoolField(Required = true, Index = true)]
    public virtual bool NoIndex { get; set; }

    [BoolField(Required = true, Index = true)]
    public virtual bool IsStartPage { get; set; }

    [BoolField(Required = true, Index = true)]
    public virtual bool IsFooterPage { get; set; }

    [BlockField]
    public virtual BlockField MainContent { get; set; }
}
