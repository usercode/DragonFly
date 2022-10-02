﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.BlockField;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.BlockField.Razor;

/// <summary>
/// BlockComponent
/// </summary>
/// <typeparam name="TBlock"></typeparam>
public class BlockComponent<TBlock> : ComponentBase, IBlockComponent
    where TBlock : Block
{
    [Parameter]
    public TBlock Block { get; set; }

    Block IBlockComponent.Block => Block;

}
