// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using Microsoft.AspNetCore.Components;

namespace DragonFly.Client;

public interface IAssetMetadataComponent : IComponent
{
    AssetMetadata Metadata { get; }

    Type MetadataType { get; }
}
