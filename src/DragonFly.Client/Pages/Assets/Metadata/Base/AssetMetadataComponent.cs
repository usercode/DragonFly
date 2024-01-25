// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using Microsoft.AspNetCore.Components;

namespace DragonFly.Client;

public abstract class AssetMetadataComponent<T> : ComponentBase, IAssetMetadataComponent
    where T : AssetMetadata
{
    [Parameter]
    public T Metadata { get; set; }

    public Type MetadataType => typeof(T);

    AssetMetadata IAssetMetadataComponent.Metadata => Metadata;
}
