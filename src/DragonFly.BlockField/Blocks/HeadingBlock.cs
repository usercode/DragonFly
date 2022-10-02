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
/// HeadingBlock
/// </summary>
public class HeadingBlock : Block
{
    public override string CssIcon => "fa-solid fa-heading";

    public string? Text { get; set; }

    public HeadingType HeadingType { get; set; }
}
