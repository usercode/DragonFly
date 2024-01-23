// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using Microsoft.AspNetCore.Components;

namespace DragonFly.Client;

public abstract class AssetMetadataComponent<T> : ComponentBase, IAssetMetadataComponent<T>
    where T : AssetMetadata
{
    [Parameter]
    public T Metadata { get; set; }

    AssetMetadata IAssetMetadataComponent.Metadata => Metadata;

    Type IAssetMetadataComponent.MetadataType => typeof(T);
}
