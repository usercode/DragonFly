// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Net.Http.Json;
using DragonFly.Query;
using SmartResults;

namespace DragonFly.API.Client;

/// <summary>
/// ContentApiStorage
/// </summary>
internal class ContentApiStorage : IContentStorage
{
    public ContentApiStorage(HttpClient client)
    {
        Client = client;
    }

    private HttpClient Client { get; }

    public async Task<Result<ContentItem?>> GetContentAsync(ContentId id)
    {
        var result = await Client
                            .GetAsync($"api/content/{id.Schema}/{id.Id}")
                            .ToResultAsync<RestContentItem>(ApiJsonSerializerDefault.Options);

        return result.Convert(x => x?.ToModel());
    }

    public async Task<Result> CreateAsync(ContentItem entity)
    {
        var result = await Client
                            .PostAsJsonAsync($"api/content", entity.ToRest(), ApiJsonSerializerDefault.Options)
                            .ToResultAsync<ResourceCreated>(ApiJsonSerializerDefault.Options);

        if (result.IsSucceeded)
        {
            entity.Id = result.Value.Id;
        }

        return result;
    }

    public async Task<Result> UpdateAsync(ContentItem entity)
    {
        return await Client
                        .PutAsJsonAsync($"api/content", entity.ToRest(), ApiJsonSerializerDefault.Options)
                        .ToResultAsync<ResourceCreated>(ApiJsonSerializerDefault.Options);
    }

    public async Task<Result<bool>> DeleteAsync(ContentId id)
    {
        return await Client
                        .DeleteAsync($"api/content/{id.Schema}/{id.Id}")
                        .ToResultAsync<bool>();
    }

    public async Task<Result<bool>> PublishAsync(ContentId id)
    {
        return await Client
                        .PostAsync($"api/content/{id.Schema}/{id.Id}/publish", new StringContent(string.Empty))
                        .ToResultAsync<bool>();
    }

    public async Task<Result<bool>> UnpublishAsync(ContentId id)
    {
        return await Client
                        .PostAsync($"api/content/{id.Schema}/{id.Id}/unpublish", new StringContent(string.Empty))
                        .ToResultAsync<bool>();
    }

    public async Task<Result<QueryResult<ContentItem>>> QueryAsync(ContentQuery queryParameters)
    {
        var result = await Client
                                .PostAsJsonAsync($"api/content/query", queryParameters, ApiJsonSerializerDefault.Options)
                                .ToResultAsync<QueryResult<RestContentItem>>(ApiJsonSerializerDefault.Options);

        return result.Convert(x => x.Convert(e => e.ToModel()));
    }

    public async Task<Result<BackgroundTaskInfo>> PublishQueryAsync(ContentQuery query)
    {
        return await Client
                        .PostAsJsonAsync($"api/content/publish", query, ApiJsonSerializerDefault.Options)
                        .ToResultAsync<BackgroundTaskInfo>(ApiJsonSerializerDefault.Options);                                
    }

    public async Task<Result<BackgroundTaskInfo>> UnpublishQueryAsync(ContentQuery query)
    {
       return await Client
                        .PostAsJsonAsync($"api/content/unpublish", query, ApiJsonSerializerDefault.Options)
                        .ToResultAsync<BackgroundTaskInfo>(ApiJsonSerializerDefault.Options);      
    }
}
