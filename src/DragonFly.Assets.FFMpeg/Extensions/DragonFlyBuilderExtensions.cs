// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Builders;
using FFMpegCore;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddVideoMetadata(this IDragonFlyBuilder builder)
    {
        builder.Services.AddTransient<IAssetProcessing, VideoProcessing>();

        return builder;
    }
}
