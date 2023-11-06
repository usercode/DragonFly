﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.API;
using DragonFly.BlockField;
using DragonFly.BlockField.Client.Pages;
using DragonFly.BlockField.Client.Pages.Blocks;
using DragonFly.Client.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.Client;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddBlockField(this IDragonFlyBuilder builder)
    {
        builder.Services.AddSingleton(BlockFieldManager.Default);

        builder.AddJsonTypeInfoResolver(BlockFieldJsonSerializerContext.Default);

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
