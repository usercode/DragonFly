using DragonFly.AspNetCore.Middleware;
using DragonFly.Client.Core.Assets;
using DragonFly.Core.Builders;
using ImageWizard;
using ImageWizard.Caches;
using ImageWizard.Client;
using ImageWizard.DocNET;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DragonFly.ImageWizard
{
    public static class DragonFlyBuilderExtensions
    {
        public static IDragonFlyBuilder AddImageWizard(this IDragonFlyBuilder builder)
        {
            builder.Services.AddTransient<IAssetPreviewUrlService, ImageWizardAssetDataUrlService>();
            builder.Services.AddTransient<IConfigureOptions<ImageWizardClientSettings>, ConfigureImageWizardClientOptions>();

            //ImageWizard
            builder.Services.AddImageWizard()
                                    .AddDragonFly()
                                    .AddImageSharp()
                                    .AddSvgNet()
                                    .AddDocNET()
                                    .SetFileCache()
                                    ;

            builder.Services.AddImageWizardClient(x => x.BaseUrl = "/dragonfly/image");

            return builder;
        }

        public static IDragonFlyFullBuilder MapImageWizard(this IDragonFlyFullBuilder builder, bool requireAuthentication = true)
        {
            if (requireAuthentication)
            {
                builder.Builder(x => x.UseImageWizard());
            }
            else
            {
                builder.PreAuthBuilder(x => x.UseImageWizard());
            }

            return builder;
        }
    }
}
