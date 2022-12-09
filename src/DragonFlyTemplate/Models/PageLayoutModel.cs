// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;
using DragonFly.Proxy.Attributes;
using DragonFly.BlockField;

namespace DragonFlyTemplate.Models;

[ContentItem("_PageLayout")]
public class PageLayoutModel : EntityPageModel
{
    [StringField(Required = true, ListField = true, Searchable = true)]
    public virtual string Name { get; set; }

    [HtmlField]
    public virtual HtmlField Header { get; set; }

    [HtmlField]
    public virtual HtmlField Footer { get; set; }

}
