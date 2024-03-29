﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Net.Http.Json;
using DragonFly.Query;

namespace DragonFly.API.Client;

/// <summary>
/// ContentService
/// </summary>
public partial class ClientContentService : IContentStorage
{       
    public async Task<ContentItem?> GetContentAsync(string schema, Guid id)
    {
        RestContentItem? entity = await Client.GetFromJsonAsync<RestContentItem>($"api/content/{schema}/{id}", ApiJsonSerializerDefault.Options);

        if (entity == null)
        {
            return null;
        }

        return entity.ToModel();
    }

    public async Task CreateAsync(ContentItem entity)
    {
        var response = await Client.PostAsJsonAsync($"api/content", entity.ToRest(), ApiJsonSerializerDefault.Options);

        response.EnsureSuccessStatusCode();

        ResourceCreated? result = await response.Content.ReadFromJsonAsync<ResourceCreated>(ApiJsonSerializerDefault.Options);

        if (result == null)
        {
            throw new Exception();
        }

        entity.Id = result.Id;
    }

    public async Task UpdateAsync(ContentItem entity)
    {
        var response = await Client.PutAsJsonAsync($"api/content", entity.ToRest(), ApiJsonSerializerDefault.Options);

        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(ContentItem entity)
    {
        var response = await Client.DeleteAsync($"api/content/{entity.Schema.Name}/{entity.Id}");

        response.EnsureSuccessStatusCode();
    }

    public async Task PublishAsync(ContentItem entity)
    {
        var response = await Client.PostAsync($"api/content/{entity.Schema.Name}/{entity.Id}/publish", new StringContent(string.Empty));

        response.EnsureSuccessStatusCode();
    }

    public async Task UnpublishAsync(ContentItem entity)
    {
        var response = await Client.PostAsync($"api/content/{entity.Schema.Name}/{entity.Id}/unpublish", new StringContent(string.Empty));

        response.EnsureSuccessStatusCode();
    }

    public async Task<QueryResult<ContentItem>> QueryAsync(ContentQuery queryParameters)
    {
        HttpResponseMessage response = await Client.PostAsJsonAsync($"api/content/query", queryParameters, ApiJsonSerializerDefault.Options);

        QueryResult<RestContentItem>? queryResult = await response.Content.ReadFromJsonAsync<QueryResult<RestContentItem>>(ApiJsonSerializerDefault.Options) ?? throw new ArgumentNullException();

        return queryResult.Convert(x => x.ToModel());
    }

    public async Task<IBackgroundTaskInfo> PublishQueryAsync(ContentQuery query)
    {
        var response = await Client.PostAsJsonAsync($"api/content/publish", query, ApiJsonSerializerDefault.Options);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<BackgroundTaskInfo>(ApiJsonSerializerDefault.Options) ?? throw new ArgumentNullException();
    }

    public async Task<IBackgroundTaskInfo> UnpublishQueryAsync(ContentQuery query)
    {
        var response = await Client.PostAsJsonAsync($"api/content/unpublish", query, ApiJsonSerializerDefault.Options);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<BackgroundTaskInfo>(ApiJsonSerializerDefault.Options) ?? throw new ArgumentNullException();
    }
}
