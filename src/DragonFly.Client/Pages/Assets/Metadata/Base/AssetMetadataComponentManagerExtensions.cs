// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client;
using Microsoft.AspNetCore.Components;
using System;

namespace DragonFly;

/// <summary>
/// ComponentManagerExtensions
/// </summary>
public static class AssetMetadataComponentManagerExtensions
{
    public static void RegisterAssetMetadata<TMetadataComponent>(this ComponentManager componentManager)
      where TMetadataComponent : IAssetMetadataComponent
    {
        Type metadataType = typeof(TMetadataComponent).GetProperty(nameof(IAssetMetadataComponent.Metadata)).PropertyType;

        componentManager.Add(metadataType, typeof(TMetadataComponent));
    }

    public static RenderFragment CreateComponent(this ComponentManager componentManager, AssetMetadata metadata)
    {
        Type componentType = componentManager.GetComponentType(metadata.GetType());

        return builder =>
        {
            builder.OpenComponent(0, componentType);
            builder.AddAttribute(0, nameof(IAssetMetadataComponent.Metadata), metadata);
            builder.CloseComponent();
        };
    }  
}
