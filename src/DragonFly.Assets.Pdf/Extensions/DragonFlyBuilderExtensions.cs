// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
{
    /// <summary>
    /// Adds <see cref="PdfProcessing"/> service for applying <see cref="PdfMetadata"/> to matching assets.
    /// </summary>
    public static IDragonFlyBuilder AddPdfMetadata(this IDragonFlyBuilder builder)
    {
        builder.Services.AddTransient<IAssetProcessing, PdfProcessing>();

        return builder;
    }
}
