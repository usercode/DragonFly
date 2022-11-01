// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Exports;
using DragonFly.Data.Models;
using DragonFly.Models;
using System.Net.Http.Json;
using DragonFly.Storage;
using DragonFly.AspNetCore.API.Exports.Json;
using DragonFly.Query;

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

    public async Task DeleteAsync(ContentItem entity)
    {
        await Client.DeleteAsync($"api/content/{entity.Schema.Name}/{entity.Id}");
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
