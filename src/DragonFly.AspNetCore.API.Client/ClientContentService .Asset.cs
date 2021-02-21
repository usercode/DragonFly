using DragonFly.AspNetCore;
using DragonFly.AspNetCore.Exports;
using DragonFly.AspNetCore.API.Exports;
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
    public partial class ClientContentService : IAssetStorage
    {
        public async Task<QueryResult<Asset>> GetAssetsAsync(AssetQuery assetQuery)
        {
            var response = await Client.PostAsJson($"api/asset/query", assetQuery);

            return await response.Content.ParseJsonAsync<QueryResult<Asset>>();
        }

        public async Task UploadAsync(Guid id, string mimetype, Stream stream)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"api/asset/{id}/upload");
            requestMessage.Content = new StreamContent(stream);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(mimetype);

            var response = await Client.SendAsync(requestMessage);

            response.EnsureSuccessStatusCode();
        }

        public async Task<Stream> DownloadAsync(Guid id)
        {
            return await Client.GetStreamAsync($"api/asset/{id}/download");
        }

        //assets
        public async Task<Asset> GetAssetAsync(Guid id)
        {
            var response = await Client.GetAsync($"api/asset/{id}");

            return await response.Content.ParseJsonAsync<Asset>();
        }

        public async Task CreateAsync(Asset entity)
        {
            var response = await Client.PostAsJson($"api/asset", entity);

            var result = await response.Content.ParseJsonAsync<ResourceCreated>();

            entity.Id = result.Id;
        }

        public async Task PublishAsync(Guid id)
        {
            await Client.PostAsync($"api/asset/{id}/publish", new StringContent(string.Empty));
        }

        public async Task UpdateAsync(Asset entity)
        {
            await Client.PutAsJson($"api/asset/{entity.Id}", entity.ToRest());
        }

        public async Task DeleteAsybc(Guid id)
        {
            await Client.DeleteAsync($"api/asset/{id}");
        }
    }
}
