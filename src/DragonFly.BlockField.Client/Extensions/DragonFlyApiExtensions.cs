// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.BlockField;
using DragonFly.BlockField.Client;

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
        api.BlockFields().Add<TBlock>();
        api.Components().RegisterBlock<TBlockView>();
    }
}
