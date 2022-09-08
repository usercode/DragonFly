using DragonFly.Content;
using DragonFly.Core.Builders;
using DragonFly.Fields.BlockField.Razor.Pages;
using DragonFly.Fields.BlockField.Razor.Pages.Blocks;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.Fields.BlockField.Razor;

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
            api.RegisterBlock<UnknownBlock, UnknownBlockView>();
            api.RegisterBlock<CodeBlock, CodeBlockView>();
            api.RegisterBlock<OpenGraphBlock, OpenGraphView>();
            api.RegisterBlock<HeadingBlock, HeadingBlockView>();
        });

        return builder;
    }
}
