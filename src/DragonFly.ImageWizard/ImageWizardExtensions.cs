// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using ImageWizard;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.ImageWizard;

public static class ImageWizardExtensions
{
    public static IImageWizardBuilder AddDragonFlyLoader(this IImageWizardBuilder wizardConfiguration)
    {
        wizardConfiguration.Services.AddTransient<DragonFlyLoader>();
        wizardConfiguration.LoaderManager.Register<DragonFlyLoader>("dragonfly");

        return wizardConfiguration;
    }
}
