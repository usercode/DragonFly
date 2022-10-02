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
/// TextBlock
/// </summary>
public class TextBlock : Block
{
    public override string CssIcon => "fa-solid fa-align-left";

    public string? Text { get; set; }
}
