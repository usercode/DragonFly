// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components;

namespace DragonFly.Assets;

public abstract class AssetMetadataComponent<T> : ComponentBase, IAssetMetadataComponent
    where T : AssetMetadata
{
    [Parameter]
    public T Metadata { get; set; }

    AssetMetadata IAssetMetadataComponent.Metadata => Metadata;
}
