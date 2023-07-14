// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Builders;
using DragonFly.BlockField;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
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
    public static IDragonFlyBuilder AddBlockField(this IDragonFlyBuilder builder)
    {
        builder.Services.AddSingleton(BlockFieldManager.Default);

        builder.Init(api =>
        {
            api.BlockField().AddDefaults();
            api.ContentField().Add<BlockField.BlockField>();
        });

        return builder;
    }
}
