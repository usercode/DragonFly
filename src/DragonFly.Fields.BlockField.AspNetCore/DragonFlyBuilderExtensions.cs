using DragonFly.Content;
using DragonFly.Core.Builders;
using DragonFly.Fields.BlockField;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddBlockField(this IDragonFlyBuilder builder)
    {
        builder.Services.AddSingleton(BlockFieldManager.Default);

        builder.Init(api =>
        {
            api.ContentField().Add<BlockField>();

            //api.RegisterBlock<ColumnBlock>();
            //api.RegisterBlock<AssetBlock, AssetBlockView>();
            //api.RegisterBlock<TextBlock, TextBlockView>();
            //api.RegisterBlock<HtmlBlock, HtmlBlockView>();
            //api.RegisterBlock<YouTubeBlock, YouTubeBlockView>();
            //api.RegisterBlock<UnknownBlock, UnknownBlockView>();
        });

        return builder;
    }
}
