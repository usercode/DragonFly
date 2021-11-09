using ImageWizard.Client;
using ImageWizard.Core.Middlewares;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.ImageWizard
{
    public static class Extensions
    {
        public static IImageWizardBuilder AddDragonFly(this IImageWizardBuilder wizardConfiguration)
        {
            wizardConfiguration.Services.AddTransient<DragonFlyImageLoader>();
            wizardConfiguration.ImageLoaderManager.Register<DragonFlyImageLoader>("dragonfly");

            wizardConfiguration.Services.AddImageWizardClient();

            return wizardConfiguration;
        }
    }
}
