using DragonFly.Content;
using DragonFly.Fields.BlockField.Blocks;
using DragonFly.Fields.BlockField.Razor.Pages;
using DragonFly.Fields.BlockField.Razor.Pages.Blocks;
using DragonFly.Razor.Builder;
using DragonFly.Razor.Services;
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
            ContentFieldManager.Default.RegisterField<BlockField>();

            ComponentManager.Default.RegisterField<BlockFieldView>();

            ComponentManager.Default.RegisterElement<BlockElementView>();
            ComponentManager.Default.RegisterElement<ImageElementView>();

            BlockFieldManager.Default.RegisterElement<BlockElement>();
            BlockFieldManager.Default.RegisterElement<ImageElement>();

            return builder;
        }
    }
}
