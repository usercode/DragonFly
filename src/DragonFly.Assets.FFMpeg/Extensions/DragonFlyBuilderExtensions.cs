// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
{
    /// <summary>
    /// Adds <see cref="VideoProcessing"/> service for applying <see cref="VideoMetadata"/> to matching assets.
    /// </summary>
    public static IDragonFlyBuilder AddVideoMetadata(this IDragonFlyBuilder builder)
    {
        builder.Services.AddTransient<IAssetProcessing, VideoProcessing>();

        return builder;
    }
}
