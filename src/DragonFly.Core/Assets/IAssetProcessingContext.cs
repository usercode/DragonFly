using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly
{
    /// <summary>
    /// IAssetProcessingContext
    /// </summary>
    public interface IAssetProcessingContext
    {
        /// <summary>
        /// Asset
        /// </summary>
        Asset Asset { get; }

        Task AddMetadataAsync(AssetMetadata metadata);

        Task<Stream> OpenAssetStreamAsync();
    }
}
