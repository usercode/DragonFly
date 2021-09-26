using DragonFly.Content;
using DragonFly.Core.Builders;
using DragonFly.Fields.BlockField.Blocks;
using DragonFly.Storage.MongoDB.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField.Storage
{
    public static class StartupExtensions
    {
        public static IDragonFlyBuilder AddBlockField(this IDragonFlyBuilder builder)
        {
            ContentFieldManager.Default.RegisterField<BlockField>();

            MongoFieldManager.Default.Register<BlockFieldSerializer>();

            BlockFieldManager.Default.RegisterElement<BlockElement>();
            BlockFieldManager.Default.RegisterElement<ImageElement>();
            BlockFieldManager.Default.RegisterElement<HtmlElement>();

            return builder;
        }
    }
}
