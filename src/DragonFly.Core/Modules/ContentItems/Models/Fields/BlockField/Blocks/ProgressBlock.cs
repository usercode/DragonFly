﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ProgressBlock
/// </summary>
public class ProgressBlock : Block
{
    public ProgressBlock()
    {
    }

    public ProgressBlock(ColorType colorType, int? value)
        : this()
    {
        ColorType = colorType;
        Value = value;
    }

    public override string CssIcon => "fa-solid fa-bars-progress";

    public int? Value { get; set; }

    public ColorType ColorType { get; set; } = ColorType.Primary;
}
