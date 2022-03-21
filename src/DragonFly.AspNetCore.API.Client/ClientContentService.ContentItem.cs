using DragonFly.AspNetCore;
using DragonFly.AspNetCore.Exports;
using DragonFly.AspNetCore.API.Exports;
using DragonFly.AspNetCore.API.Models;
using DragonFly.Content;
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
using DragonFly.Storage;
using DragonFly.AspNetCore.API.Exports.Json;

namespace DragonFly.Client;

/// <summary>
/// ContentService
/// </summary>
public partial class ClientContentService : IContentStorage
{       
    public async Task<ContentItem> GetContentAsync(string schema, Guid id)
    {
        var response = await Client.GetAsync($"api/content/{schema}/{id}");

        response.EnsureSuccessStatusCode();

        var e = await response.Content.ReadFromJsonAsync<RestContentItem>();

        return e.ToModel();
    }

    public async Task CreateAsync(ContentItem entity)
    {
        var response = await Client.PostAsJsonAsync($"api/content", entity.ToRest());

        var result = await response.Content.ReadFromJsonAsync<ResourceCreated>();

        entity.Id = result.Id;
    }

    public async Task UpdateAsync(ContentItem entity)
    {
        await Client.PutAsJsonAsync($"api/content", entity.ToRest());
    }

    public async Task PublishAsync(string schema, Guid id)
    {
        var response = await Client.PostAsync($"api/content/{schema}/{id}/publish", new StringContent(string.Empty));

        response.EnsureSuccessStatusCode();
    }

    public async Task UnpublishAsync(string schema, Guid id)
    {
        var response = await Client.PostAsync($"api/content/{schema}/{id}/unpublish", new StringContent(string.Empty));

        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(string schema, Guid id)
    {
        var response = await Client.DeleteAsync($"api/content/{schema}/{id}");

        response.EnsureSuccessStatusCode();
    }

    public async Task<QueryResult<ContentItem>> QueryAsync(ContentItemQuery queryParameters)
    {
        HttpResponseMessage response = await Client.PostAsJsonAsync($"api/content/query", queryParameters, JsonSerializerDefault.Options);

        var queryResult = await response.Content.ReadFromJsonAsync<QueryResult<RestContentItem>>();

        return new QueryResult<ContentItem>()
        {
            Offset = queryResult.Offset,
            Count = queryResult.Count,
            TotalCount = queryResult.TotalCount,
            Items = queryResult.Items.Select(x => x.ToModel()).ToList()
        };
    }

    public async Task PublishQueryAsync(ContentItemQuery query)
    {
        var response = await Client.PostAsJsonAsync($"api/content/publish", query, JsonSerializerDefault.Options);

        response.EnsureSuccessStatusCode();
    }
}
