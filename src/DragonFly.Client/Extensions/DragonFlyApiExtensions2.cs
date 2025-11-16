// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using DragonFly.Client;

namespace DragonFly;

/// <summary>
/// DragonFlyApiExtensions
/// </summary>
public static class DragonFlyApiExtensions2
{
    /// <summary>
    /// Registers the <typeparamref name="TBlock"/> with <typeparamref name="TBlockView"/>.
    /// </summary>
    public static void RegisterBlock<TBlock, TBlockView>(this IDragonFlyApi api)
        where TBlock : Block, new()
        where TBlockView : BlockComponent<TBlock>, new()
    {
        api.Blocks.Add<TBlock>();

        Type elementType = new TBlockView().BlockType;

        api.Components.Add(elementType, typeof(TBlockView));
    }
}
