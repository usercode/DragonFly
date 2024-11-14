// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;
using DragonFly.AspNetCore.Builders;
using FFMpegCore;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.Assets.FFMpeg;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddVideoMetadata(this IDragonFlyBuilder builder)
    {
        GlobalFFOptions.Configure(x =>
        {
            x.BinaryFolder = "C:\\Users\\admin\\Downloads";
            x.TemporaryFilesFolder = "C:\\Users\\admin\\Downloads";
        });

        builder.Services.AddTransient<IAssetProcessing, VideoProcessing>();

        return builder;
    }
}
