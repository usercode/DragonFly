using DragonFly.Content;
using DragonFly.Core.Builders;
using DragonFly.Fields.BlockField;
using DragonFly.Fields.BlockField.Storage.Serializers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore
{
    public static class StartupExtensions
    {
        public static IDragonFlyBuilder AddBlockField(this IDragonFlyBuilder builder)
        {
            builder.Services.AddSingleton<DocumentSerializer>();

            builder.Init(api => api.ContentField().Add<BlockField>());

            return builder;
        }
    }
}
