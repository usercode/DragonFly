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
    Task<bool> OnAssetChangedAsync(IAssetProcessingContext context);

}
