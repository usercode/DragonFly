// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;
using DragonFly.AspNetCore.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.Assets.ImageSharp;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddImageMetadata(this IDragonFlyBuilder builder)
    {
        builder.Services.AddTransient<IAssetProcessing, ImageProcessing>();

        return builder;
    }
}
