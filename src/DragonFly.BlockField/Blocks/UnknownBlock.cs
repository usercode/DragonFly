﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.BlockField;

/// <summary>
/// UnknownBlock
/// </summary>
public class UnknownBlock : Block
{
    public UnknownBlock()
    {

    }

    public UnknownBlock(string? content)
    {
        Content = content;
    }

    public string? Content { get; set; }
}
