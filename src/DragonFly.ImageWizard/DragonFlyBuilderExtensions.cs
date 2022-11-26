// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Middleware;
using DragonFly.Builders;
using ImageWizard;
using ImageWizard.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace DragonFly.ImageWizard;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddImageWizard(this IDragonFlyBuilder builder, Action<IImageWizardBuilder>? action = null)
    {
        builder.Services.AddTransient<IAssetPreviewUrlService, ImageWizardAssetUrlService>();
        builder.Services.AddTransient<IConfigureOptions<ImageWizardClientSettings>, ConfigureImageWizardClientOptions>();

        //ImageWizard
        IImageWizardBuilder imageWizardBuilder = builder.Services.AddImageWizard()                                                                    
                                                                    .AddImageSharp()
                                                                    .AddSvgNet()
                                                                    .AddDocNET()
                                                                    .AddDragonFlyLoader()
                                                                    .AddYoutubeLoader()
                                                                    .SetFileCache()
                                                                    ;

        action?.Invoke(imageWizardBuilder);

        builder.Services.AddImageWizardClient(x => x.BaseUrl = "/dragonfly/image");

        return builder;
    }

    public static IDragonFlyFullBuilder MapImageWizard(this IDragonFlyFullBuilder builder, bool requireAuthentication = true)
    {
        if (requireAuthentication)
        {
            builder.Builder(x => x.UseImageWizard("/image"));
        }
        else
        {
            builder.PreAuthBuilder(x => x.UseImageWizard("/image"));
        }

        return builder;
    }
}
