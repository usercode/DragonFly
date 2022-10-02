// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using ImageWizard;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.ImageWizard;

public class ConfigureImageWizardClientOptions : IConfigureOptions<ImageWizardClientSettings>
{
    public ConfigureImageWizardClientOptions(IOptions<ImageWizardOptions> imageWizardOptions)
    {
        ImageWizardOptions = imageWizardOptions.Value;
    }

    private ImageWizardOptions ImageWizardOptions { get; }

    public void Configure(ImageWizardClientSettings options)
    {
        options.Key = ImageWizardOptions.Key;
    }
}
