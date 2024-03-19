// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using Microsoft.AspNetCore.Components;

namespace DragonFly.Client;

/// <summary>
/// BlockComponent
/// </summary>
/// <typeparam name="TBlock"></typeparam>
public class BlockComponent<TBlock> : ComponentBase, IBlockComponent
    where TBlock : Block
{
    [Parameter]
    public TBlock Block { get; set; }

    public Type BlockType => typeof(TBlock);

    Block IBlockComponent.Block => Block;

    [CascadingParameter]
    public FieldComponent<BlockField> Field { get; set; }
}
