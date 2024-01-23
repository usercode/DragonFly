// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;

namespace DragonFly.Client;

public interface IAssetMetadataComponent
{
    AssetMetadata Metadata { get; }

    Type MetadataType { get; }
}
