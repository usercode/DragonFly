// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.BlockField;
using DragonFly.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.Core;

public static class DragonFlyCoreBuilderExtensions
{
    /// <summary>
    /// Adds a block field.<br />
    /// <br />
    /// Default blocks: <br />
    /// <see cref="ColumnBlock"/>, <see cref="ContainerBlock"/> <br />
    /// <see cref="AssetBlock"/>, <see cref="ReferenceBlock"/> <br />
    /// <see cref="HeadingBlock"/>, <see cref="TextBlock"/>, <see cref="HtmlBlock"/>, <see cref="CodeBlock"/>, <see cref="QuoteBlock"/>, <see cref="AlertBlock"/>, <see cref="ProgressBlock"/> <br/>
    /// <see cref="YouTubeBlock"/>, <see cref="OpenGraphBlock"/>
    /// </summary>
    public static TDragonFlyBuilder AddBlockFieldCore<TDragonFlyBuilder>(this TDragonFlyBuilder builder)
        where TDragonFlyBuilder : IDragonFlyBuilder
    {
        builder.AddRestSerializerResolver(BlockFieldSerializerContext.Default);

        builder.Services.AddSingleton(BlockFieldManager.Default);

        builder.Init(api =>
        {
            api.BlockField().AddDefaults();
            api.ContentField().Add<BlockField.BlockField>();
        });

        return builder;
    }
}
