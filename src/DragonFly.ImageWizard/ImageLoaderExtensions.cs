// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using DragonFly.Content;
using ImageWizard.Client;

namespace DragonFly.ImageWizard;

public static class ImageLoaderExtensions
{
    public static IFilter Asset(this ILoader loader, Asset asset)
    {
        return loader.LoadData("dragonfly", $"{asset.Id}?v={asset.Hash.AsSpan(0, 8)}");
    }
}
