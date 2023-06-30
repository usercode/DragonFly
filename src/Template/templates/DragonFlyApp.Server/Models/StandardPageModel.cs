// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;
using DragonFly.BlockField;
using DragonFly.Generator;

namespace DragonFlyTemplate.Models;

[ContentItem("StandardPage")]
public partial class StandardPageModel
{
    [StringField(Required = true, ListField = true)]
    private string? _title;

    [SlugField(Required = true, Index = true)]
    private string? _slug;

    [BoolField(Required = true, Index = true)]
    private bool? _noFollow;

    [BoolField(Required = true, Index = true)]
    private bool? _noIndex;

    [BoolField(Required = true, Index = true)]
    private bool? _isStartPage;

    [BoolField(Required = true, Index = true)]
    private bool? _isFooterPage;

    [BlockField]
    private BlockField _mainContent;
}
