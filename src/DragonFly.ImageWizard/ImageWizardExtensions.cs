using DragonFly.Content;
using ImageWizard.Client;
using ImageWizard.Client.Builder.Types;
using ImageWizard.Core.Middlewares;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.ImageWizard
{
    public static class ImageWizardExtensions
    {
        public static IImageWizardBuilder AddDragonFly(this IImageWizardBuilder wizardConfiguration)
        {
            wizardConfiguration.Services.AddTransient<DragonFlyImageLoader>();
            wizardConfiguration.ImageLoaderManager.Register<DragonFlyImageLoader>("dragonfly");

            return wizardConfiguration;
        }

        public static IImageFilters Asset(this IImageLoaderType imageUrlBuilder, Asset asset)
        {
            return imageUrlBuilder.Image("dragonfly", $"{asset.Id}?v={asset.Hash}");
        }
    }
}
