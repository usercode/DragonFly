using DragonFly.Content;
using DragonFly.Fields.BlockField.Blocks;
using DragonFly.Fields.BlockField.Razor.Pages;
using DragonFly.Fields.BlockField.Razor.Pages.Blocks;
using DragonFly.Razor.Builder;
using DragonFly.Razor.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField.Razor
{
    public static class StartupExtensions
    {
        public static IDragonFlyClientBuilder AddBlockField(this IDragonFlyClientBuilder builder)
        {
            builder.WebAssemblyHostBuilder.Services.AddSingleton(BlockFieldManager.Default);

            DragonFlyApi.Default.RegisterField<BlockField, BlockFieldView>();

            DragonFlyApi.Default.RegisterBlock<ColumnBlock, ColumnBlockView>();
            DragonFlyApi.Default.RegisterBlock<ImageBlock, ImageBlockView>();
            DragonFlyApi.Default.RegisterBlock<TextBlock, TextBlockView>();
            DragonFlyApi.Default.RegisterBlock<HtmlBlock, HtmlBlockView>();

            return builder;
        }
    }
}
