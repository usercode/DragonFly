// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Net.Http.Json;
using DragonFly.Query;
using SmartResults;

namespace DragonFly.API.Client;

/// <summary>
/// ContentItemApiStorage
/// </summary>
internal class ContentItemApiStorage : IContentStorage
{
    public ContentItemApiStorage(RestApiClient client)
    {
        Client = client.Http;
    }

    private HttpClient Client { get; }

    public async Task<Result<ContentItem?>> GetContentAsync(string schema, Guid id)
    {
        var result = await Client
                            .GetAsync($"api/content/{schema}/{id}")
                            .ReadResultFromJsonAsync<RestContentItem>(ApiJsonSerializerDefault.Options);

        return result.ToResult(x => x?.ToModel());
    }

    public async Task<Result> CreateAsync(ContentItem entity)
    {
        var result = await Client
                            .PostAsJsonAsync($"api/content", entity.ToRest(), ApiJsonSerializerDefault.Options)
                            .ReadResultFromJsonAsync<ResourceCreated>(ApiJsonSerializerDefault.Options);

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
                        .ReadResultFromJsonAsync();
    }

    public async Task<Result<bool>> DeleteAsync(string schema, Guid id)
    {
        return await Client
                        .DeleteAsync($"api/content/{schema}/{id}")
                        .ReadResultFromJsonAsync<bool>();
    }

    public async Task<Result<bool>> PublishAsync(string schema, Guid id)
    {
        return await Client
                        .PostAsync($"api/content/{schema}/{id}/publish", new StringContent(string.Empty))
                        .ReadResultFromJsonAsync<bool>();
    }

    public async Task<Result<bool>> UnpublishAsync(string schema, Guid id)
    {
        return await Client
                        .PostAsync($"api/content/{schema}/{id}/unpublish", new StringContent(string.Empty))
                        .ReadResultFromJsonAsync<bool>();
    }

    public async Task<Result<QueryResult<ContentItem>>> QueryAsync(ContentQuery queryParameters)
    {
        var result = await Client
                                .PostAsJsonAsync($"api/content/query", queryParameters, ApiJsonSerializerDefault.Options)
                                .ReadResultFromJsonAsync<QueryResult<RestContentItem>>(ApiJsonSerializerDefault.Options);

        return result.ToResult(x => x.Convert(e => e.ToModel()));
    }

    public async Task<Result<BackgroundTaskInfo>> PublishQueryAsync(ContentQuery query)
    {
        return await Client
                        .PostAsJsonAsync($"api/content/publish", query, ApiJsonSerializerDefault.Options)
                        .ReadResultFromJsonAsync<BackgroundTaskInfo>(ApiJsonSerializerDefault.Options)
;
    }

    public async Task<Result<BackgroundTaskInfo>> UnpublishQueryAsync(ContentQuery query)
    {
       return await Client
                        .PostAsJsonAsync($"api/content/unpublish", query, ApiJsonSerializerDefault.Options)
                        .ReadResultFromJsonAsync<BackgroundTaskInfo>(ApiJsonSerializerDefault.Options);
    }

    public async Task<Result<ContentReferenceIndex>> GetReferencedByAsync(string schema, Guid id)
    {
        var result = await Client
                            .GetAsync($"api/content/{schema}/{id}/referencedBy")
                            .ReadResultFromJsonAsync<ContentReferenceIndex>(ApiJsonSerializerDefault.Options);

        return result;
    }

    public async Task<Result> RebuildDatabaseAsync()
    {
        await Client
                       .PostAsync($"api/content/rebuildDatabase", null);

        return Result.Ok();
    }
}
