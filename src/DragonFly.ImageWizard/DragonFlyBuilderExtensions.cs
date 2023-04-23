// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Builders;
using DragonFly.ImageWizard;
using ImageWizard;
using ImageWizard.Client;
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
                                                                    .AddOpenGraphLoader()
                                                                    .SetFileCache()
                                                                    ;

        action?.Invoke(imageWizardBuilder);

        builder.Services.AddImageWizardClient(x => x.BaseUrl = "/dragonfly/image");

        return builder;
    }

    public static IDragonFlyMiddlewareBuilder MapImageWizard(this IDragonFlyMiddlewareBuilder builder, bool requireAuthentication = true)
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
