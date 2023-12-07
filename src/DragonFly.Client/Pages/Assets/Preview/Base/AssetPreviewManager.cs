// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace DragonFly.Client;

/// <summary>
/// AssetPreviewManager
/// </summary>
public sealed class AssetPreviewManager
{
    /// <summary>
    /// Default
    /// </summary>
    public static AssetPreviewManager Default { get; } = new AssetPreviewManager();

    private IDictionary<string, Type> _cache = new Dictionary<string, Type>();

    public void Add<TAssetPreview>(params string[] mimetypes)
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
