// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components;

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
