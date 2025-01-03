﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Builders;
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

        return builder;
    }
}
