// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;
using DragonFly.Proxy.Attributes;
using DragonFly.BlockField;

namespace DragonFlyTemplate.Models;

[ContentItem("_PageElement")]
public class PageElementModel : EntityPageModel
{
    [HtmlField]
    public virtual HtmlField Header { get; set; }

    [HtmlField]
    public virtual HtmlField Footer { get; set; }

}
