using DragonFly.AspNetCore;
using DragonFly.AspNetCore.Exports;
using DragonFly.AspNetCore.API.Exports;
using DragonFly.AspNetCore.REST.Models;
using DragonFly.Content;
using DragonFly.Contents.Assets;
using DragonFly.Contents.Content;
using DragonFly.Core;
using DragonFly.Core.WebHooks.Types;
using DragonFly.Data;
using DragonFly.Data.Models;
using DragonFly.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DragonFly.Content.Queries;

namespace DragonFly.Client
{
    /// <summary>
    /// ContentService
    /// </summary>
    public partial class ClientContentService : IContentStorage
    {
       
        public async Task<ContentItem> GetContentItemAsync(string schema, Guid id)
        {
            var response = await Client.GetAsync($"api/content/{schema}/{id}");

            response.EnsureSuccessStatusCode();

            var e = await response.Content.ParseJsonAsync<RestContentItem>();

            return e.ToModel();
        }

        public async Task CreateAsync(ContentItem entity)
        {
            var response = await Client.PostAsJson($"api/content/{entity.Schema.Name}", entity.ToRest());

            var result = await response.Content.ParseJsonAsync<ResourceCreated>();

            entity.Id = result.Id;
        }

        public async Task UpdateAsync(ContentItem entity)
        {
            await Client.PutAsJson($"api/content/{entity.Schema.Name}/{entity.Id}", entity.ToRest());
        }

        public async Task PublishAsync(string schema, Guid id)
        {
            var response = await Client.PostAsync($"api/content/{schema}/{id}/publish", new StringContent(string.Empty));

            response.EnsureSuccessStatusCode();
        }

        public async Task UnpublishAsync(string schema, Guid id)
        {
            await Client.PostAsync($"api/content/{schema}/{id}/unpublish", new StringContent(string.Empty));
        }

        public async Task DeleteAsync(string schema, Guid id)
        {
            await Client.DeleteAsync($"api/content/{schema}/{id}");
        }

        public async Task<QueryResult<ContentItem>> Query(string schema, QueryParameters queryParameters)
        {
            HttpResponseMessage response = await Client.PostAsJsonAsync($"api/content/{schema}/query", queryParameters);

            var queryResult = await response.Content.ParseJsonAsync<QueryResult<RestContentItem>>();

            return new QueryResult<ContentItem>() 
            { 
                Offset = queryResult.Offset,
                Count = queryResult.Count,
                TotalCount = queryResult.TotalCount,
                Items = queryResult.Items.Select(x => x.ToModel()).ToList()
            };
        }
    }
}
