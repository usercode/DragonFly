// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Builders;
using DragonFly.Assets.ImageSharp;
using DragonFly.Core.Modules.Assets.Actions;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
{
    /// <summary>
    /// Adds <see cref="ImageProcessing"/> service for applying <see cref="ImageMetadata"/> to matching assets.
    /// </summary>
    public static IDragonFlyBuilder AddImageMetadata(this IDragonFlyBuilder builder)
    {
        builder.Services.AddTransient<IAssetProcessing, ImageProcessing>();
        builder.Services.AddTransient<CropWhitespacesAssetAction>();

        AssetActionManager.Default.Add<CropWhitespacesAssetAction>("CropWhiteSpaces", MimeTypes.Images);

        return builder;
    }
}
