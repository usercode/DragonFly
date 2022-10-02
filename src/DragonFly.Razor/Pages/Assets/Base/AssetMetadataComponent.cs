// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Content;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Assets;

public abstract class AssetMetadataComponent<T> : ComponentBase, IAssetMetadataComponent
    where T : AssetMetadata
{
    [Parameter]
    public T Metadata { get; set; }

    AssetMetadata IAssetMetadataComponent.Metadata => Metadata;
}
