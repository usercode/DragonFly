﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Builders;
using DragonFly.ImageWizard;
using ImageWizard;
using ImageWizard.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
{
    /// <summary>
    /// Adds the ImageWizard service to transform assets like resizing images or converting pdf files to image.
    /// <br /><br />
    /// Default services:<br />
    /// <see cref="IAssetPreviewUrlService"/> -> <see cref="ImageWizardAssetUrlService"/>
    /// </summary>
    public static IDragonFlyBuilder AddImageWizard(this IDragonFlyBuilder builder, Action<IImageWizardBuilder>? action = null, bool requireAuthorization = true)
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
                                                                    .AddOpenGraphLoader()
                                                                    .SetFileCache()
                                                                    ;

        action?.Invoke(imageWizardBuilder);

        builder.Services.AddImageWizardClient(x => x.BaseUrl = "/dragonfly/image");

        if (requireAuthorization)
        {
            builder.AddEndpoint(x => x.MapImageWizard().RequirePermission());
        }
        else
        {
            builder.AddEndpoint(x => x.MapImageWizard().AllowAnonymous());
        }

        return builder;
    }
}
