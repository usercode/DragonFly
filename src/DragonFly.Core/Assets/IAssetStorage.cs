using DragonFly.AspNetCore.API.Exports;
using DragonFly.Core.Assets.Queries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// IAssetStorage
    /// </summary>
    public interface IAssetStorage
    {
        Task<Asset> GetAssetAsync(Guid id);

        Task<QueryResult<Asset>> GetAssetsAsync(AssetQuery assetQuery);

        Task CreateAsync(Asset asset);

        Task UpdateAsync(Asset asset);

        Task DeleteAsync(Guid id);

        Task PublishAsync(Guid id);

        Task UploadAsync(Guid id, string mimetype, Stream stream);

        Task ApplyMetadataAsync(Guid id);

        Task<Stream> DownloadAsync(Guid id);
    }
}
