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
    /// SupportedMimetypes
    /// </summary>
    /// <param name="asset"></param>
    /// <returns></returns>
    IEnumerable<string> SupportedMimetypes { get; }

    /// <summary>
    /// OnAssetChangedAsync
    /// </summary>
    /// <param name="asset"></param>
    /// <param name="openStream"></param>
    /// <returns></returns>
    Task<bool> OnAssetChangedAsync(IAssetProcessingContext context);

}
