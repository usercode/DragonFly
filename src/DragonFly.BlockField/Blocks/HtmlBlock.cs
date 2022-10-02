// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.BlockField;

/// <summary>
/// HtmlBlock
/// </summary>
public class HtmlBlock : Block
{
    public override string CssIcon => "fa-regular fa-file-code";

    public string? HtmlText { get; set; }
}
