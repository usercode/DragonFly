// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Assets;

public delegate Task<Stream> OpenAssetStream();

/// <summary>
/// IAssetAction
/// </summary>
public interface IAssetAction
{
    /// <summary>
    /// ProcessAsync
    /// </summary>
    Task<bool> ProcessAsync(IAssetActionContext context);
}
