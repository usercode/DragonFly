﻿using DragonFly.AspNetCore;
using DragonFly.AspNetCore.Exports;
using DragonFly.AspNetCore.API.Models.Assets;
using DragonFly.AspNetCore.API.Models;
using DragonFly.Content;
using DragonFly.Contents.Content;
using DragonFly.Core;
using DragonFly.Core.Assets;
using DragonFly.Core.Assets.Queries;
using DragonFly.Data;
using DragonFly.Data.Models;
using DragonFly.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DragonFly.Client
{
    /// <summary>
    /// ContentService
    /// </summary>
    public partial class ClientContentService : IAssetFolderStorage
    {
        public Task CreateAsync(AssetFolder folder)
        {
            throw new NotImplementedException();
        }

        public Task<AssetFolder> GetAssetFolderAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AssetFolder>> GetAssetFoldersAsync(AssetFolderQuery query)
        {
            var response = await Client.PostAsJson("api/assetfolder/query", query);

            response.EnsureSuccessStatusCode();

            IEnumerable<RestAssetFolder> result = await response.Content.ParseJsonAsync<IEnumerable<RestAssetFolder>>();

            return result.Select(x => x.ToModel()).ToList();
        }

        public Task UpdateAsync(AssetFolder folder)
        {
            throw new NotImplementedException();
        }
    }
}
