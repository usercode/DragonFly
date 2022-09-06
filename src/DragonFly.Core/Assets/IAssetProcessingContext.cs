using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

/// <summary>
/// IAssetProcessingContext
/// </summary>
public interface IAssetProcessingContext
{
    /// <summary>
    /// Asset
    /// </summary>
    Asset Asset { get; }

    /// <summary>
    /// SetMetadataAsync
    /// </summary>
    /// <param name="metadata"></param>
    /// <returns></returns>
    Task SetMetadataAsync(AssetMetadata metadata);

    /// <summary>
    /// OpenAssetStreamAsync
    /// </summary>
    /// <returns></returns>
    Task<Stream> OpenAssetStreamAsync();
}
