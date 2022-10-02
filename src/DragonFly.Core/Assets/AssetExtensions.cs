// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

public static class AssetDataUrlService
{
    public static string GetDataUrl(this Asset asset)
    {
        return $"/dragonfly/api/asset/{asset.Id}/download?v={asset.Hash}";
    }

    public static string GetFileSize(this Asset asset)
    {
        return $"{(double)asset.Size / 1024:###,###,##0.00} KB";
    }
}
