// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.BlockField;
using DragonFly.BlockField.Razor;

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
