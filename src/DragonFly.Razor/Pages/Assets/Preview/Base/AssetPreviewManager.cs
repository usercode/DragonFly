// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace DragonFly.Razor.Assets;

public class AssetPreviewManager
{
    private static AssetPreviewManager _default;

    public static AssetPreviewManager Default
    {
        get
        {
            if (_default == null)
            {
                _default = new AssetPreviewManager();
            }

            return _default;
        }
    }

    private IDictionary<string, Type> _cache;

    private AssetPreviewManager()
    {
        _cache = new Dictionary<string, Type>();
    }

    public void Register<TAssetPreview>(params string[] mimetypes)
        where TAssetPreview : IAssetPreviewComponent
    {
        foreach (string mimetype in mimetypes)
        {
            _cache[mimetype] = typeof(TAssetPreview);
        }
    }

    public RenderFragment CreateComponent(Asset asset)
    {
        if (_cache.TryGetValue(asset.MimeType, out Type componentType))
        {
            return builder =>
            {
                builder.OpenComponent(0, componentType);
                builder.AddAttribute(0, nameof(IAssetPreviewComponent.Asset), asset);
                builder.CloseComponent();
            };
        }
        else
        {
            return builder => { builder.OpenElement(0, "p"); builder.AddContent(0, $"Preview for asset isn't available."); builder.CloseElement(); };
        }
    }
}
