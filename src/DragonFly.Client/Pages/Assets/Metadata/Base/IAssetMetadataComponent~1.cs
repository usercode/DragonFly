// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Client;

public interface IAssetMetadataComponent<T> : IAssetMetadataComponent
    where T : AssetMetadata
{
    public new T Metadata { get; set; }
}
