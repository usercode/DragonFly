// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Content;
using ImageWizard;
using ImageWizard.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
