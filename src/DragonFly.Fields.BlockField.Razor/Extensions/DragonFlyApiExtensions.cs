﻿using DragonFly.Fields.BlockField;
using DragonFly.Fields.BlockField.Razor;
using DragonFly.Fields.BlockField.Razor.Base;

namespace DragonFly;

/// <summary>
/// DragonFlyApiExtensions
/// </summary>
public static class DragonFlyApiExtensions
{
    public static void RegisterBlock<TBlock, TBlockView>(this IDragonFlyApi api)
        where TBlock : Block, new()
        where TBlockView : BlockComponent<TBlock>
    {
        api.BlockField().Add<TBlock>();
        api.Component().RegisterBlock<TBlockView>();
    }
}
