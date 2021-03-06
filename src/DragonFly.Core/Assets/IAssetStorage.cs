﻿using DragonFly.AspNetCore.API.Exports;
using DragonFly.Core.Assets.Queries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    public interface IAssetStorage
    {
        Task<Asset> GetAssetAsync(Guid id);

        Task<QueryResult<Asset>> GetAssetsAsync(AssetQuery assetQuery);

        Task CreateAsync(Asset asset);

        Task UpdateAsync(Asset asset);

        Task DeleteAsybc(Guid id);

        Task PublishAsync(Guid id);

        Task UploadAsync(Guid id, string mimetype, Stream stream);

        Task<Stream> DownloadAsync(Guid id);
    }
}
