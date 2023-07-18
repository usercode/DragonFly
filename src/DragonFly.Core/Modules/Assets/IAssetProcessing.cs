// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// IAssetProcessing
/// </summary>
public interface IAssetProcessing
{
    /// <summary>
    /// CanUse
    /// </summary>
    bool CanUse(string mimeType);

    /// <summary>
    /// OnAssetChangedAsync
    /// </summary>
    /// <param name="asset"></param>
    /// <param name="openStream"></param>
    /// <returns></returns>
    Task<bool> OnAssetChangedAsync(IAssetProcessingContext context);

}
