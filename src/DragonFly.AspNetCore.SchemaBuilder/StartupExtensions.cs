using DragonFly.Core.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.SchemaBuilder
{
    public static class StartupExtensions
    {
        public static IDragonFlyBuilder AddSchemaBuilder(this IDragonFlyBuilder builder)
        {


            return builder;
        }
    }
}
