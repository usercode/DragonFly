using DragonFly.BlockField.Razor.Pages;
using DragonFly.BlockField.Razor.Pages.Blocks;
using DragonFly.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.BlockField.Razor;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddBlockField(this IDragonFlyBuilder builder)
    {
        builder.Services.AddSingleton(BlockFieldManager.Default);

        builder.Init(api =>
        {
            api.RegisterField<BlockField, BlockFieldView>();

            api.RegisterBlock<ColumnBlock, ColumnBlockView>();
            api.RegisterBlock<AssetBlock, AssetBlockView>();
            api.RegisterBlock<TextBlock, TextBlockView>();
            api.RegisterBlock<HtmlBlock, HtmlBlockView>();
            api.RegisterBlock<YouTubeBlock, YouTubeBlockView>();            
            api.RegisterBlock<CodeBlock, CodeBlockView>();
            api.RegisterBlock<OpenGraphBlock, OpenGraphView>();
            api.RegisterBlock<HeadingBlock, HeadingBlockView>();
            api.RegisterBlock<QuoteBlock, QuoteBlockView>();
            api.RegisterBlock<ReferenceBlock, ReferenceBlockView>();
            api.RegisterBlock<ContainerBlock, ContainerBlockView>();

            api.RegisterBlock<UnknownBlock, UnknownBlockView>();
        });

        return builder;
    }
}
