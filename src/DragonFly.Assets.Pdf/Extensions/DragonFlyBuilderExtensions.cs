// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;
using DragonFly.AspNetCore.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.Assets.Pdf;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddPdfMetadata(this IDragonFlyBuilder builder)
    {
        builder.Services.AddTransient<IAssetProcessing, PdfProcessing>();

        return builder;
    }
}
