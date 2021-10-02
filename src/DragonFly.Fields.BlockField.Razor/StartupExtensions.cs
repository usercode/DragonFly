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

            ContentFieldManager.Default.RegisterField<BlockField>();

            ComponentManager.Default.RegisterField<BlockFieldView>();

            ComponentManager.Default.RegisterBlock<ColumnBlockView>();
            ComponentManager.Default.RegisterBlock<ImageBlockView>();
            ComponentManager.Default.RegisterBlock<TextBlockView>();
            ComponentManager.Default.RegisterBlock<HtmlBlockView>();

            return builder;
        }
    }
}
