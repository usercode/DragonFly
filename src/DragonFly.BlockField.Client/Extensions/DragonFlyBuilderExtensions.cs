// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Core;
using DragonFly.BlockField;
using DragonFly.BlockField.Client.Pages;
using DragonFly.BlockField.Client.Pages.Blocks;
using DragonFly.Client.Builders;

namespace DragonFly.Client;

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
        builder.AddBlockFieldCore();

        builder.Init(api =>
        {
            api.RegisterField<BlockField.BlockField, BlockFieldView>();

            api.RegisterBlock<ColumnBlock, ColumnBlockView>();
            api.RegisterBlock<GridBlock, GridBlockView>();
            api.RegisterBlock<AssetBlock, AssetBlockView>();
            api.RegisterBlock<SlideshowBlock, SlideshowBlockView>();
            api.RegisterBlock<TextBlock, TextBlockView>();
            api.RegisterBlock<HtmlBlock, HtmlBlockView>();
            api.RegisterBlock<YouTubeBlock, YouTubeBlockView>();            
            api.RegisterBlock<CodeBlock, CodeBlockView>();
            api.RegisterBlock<OpenGraphBlock, OpenGraphView>();
            api.RegisterBlock<HeadingBlock, HeadingBlockView>();
            api.RegisterBlock<QuoteBlock, QuoteBlockView>();
            api.RegisterBlock<ReferenceBlock, ReferenceBlockView>();
            api.RegisterBlock<ContainerBlock, ContainerBlockView>();
            api.RegisterBlock<AlertBlock, AlertBlockView>();
            api.RegisterBlock<ProgressBlock, ProgressBlockView>();
            api.RegisterBlock<CardsBlock, CardsBlockView>();
            api.RegisterBlock<SectionBlock, SectionBlockView>();

            api.RegisterBlock<UnknownBlock, UnknownBlockView>();
        });

        return builder;
    }
}
