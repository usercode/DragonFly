using DragonFly.Core.Assets;
using DragonFly.Core.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.ImageSharp
{
    public static class StartupExtensions
    {
        public static IDragonFlyBuilder AddImageSharpProcessing(this IDragonFlyBuilder builder)
        {
            builder.Services.AddTransient<IAssetProcessing, ImageAssetProcessing>();

            return builder;
        }
    }
}
