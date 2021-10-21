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
            builder.Init(x => x.RegisterField<BlockField>());

            return builder;
        }
    }
}
