// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Net.Http.Json;
using DragonFly.Query;
using Results;

namespace DragonFly.API.Client;

/// <summary>
/// ContentService
/// </summary>
public partial class ClientContentService : IContentStorage
{       
    public async Task<Result<ContentItem>> GetContentAsync(string schema, Guid id)
    {
        RestContentItem? entity = await Client.GetFromJsonAsync<RestContentItem>($"api/content/{schema}/{id}", ApiJsonSerializerDefault.Options);

        ArgumentNullException.ThrowIfNull(entity);

        return entity.ToModel();
    }

    public async Task<Result> CreateAsync(ContentItem entity)
    {
        var response = await Client.PostAsJsonAsync($"api/content", entity.ToRest(), ApiJsonSerializerDefault.Options);

        response.EnsureSuccessStatusCode();

        ResourceCreated? result = await response.Content.ReadFromJsonAsync<ResourceCreated>(ApiJsonSerializerDefault.Options);

        ArgumentNullException.ThrowIfNull(result);

        entity.Id = result.Id;

        return Result.Ok();
    }

    public async Task<Result> UpdateAsync(ContentItem entity)
    {
        var response = await Client.PutAsJsonAsync($"api/content", entity.ToRest(), ApiJsonSerializerDefault.Options);

        response.EnsureSuccessStatusCode();

        return Result.Ok();
    }

    public async Task<Result> DeleteAsync(ContentItem entity)
    {
        var response = await Client.DeleteAsync($"api/content/{entity.Schema.Name}/{entity.Id}");

        response.EnsureSuccessStatusCode();

        return Result.Ok();
    }

    public async Task<Result> PublishAsync(ContentItem entity)
    {
        var response = await Client.PostAsync($"api/content/{entity.Schema.Name}/{entity.Id}/publish", new StringContent(string.Empty));

        response.EnsureSuccessStatusCode();

        return Result.Ok();
    }

    public async Task<Result> UnpublishAsync(ContentItem entity)
    {
        var response = await Client.PostAsync($"api/content/{entity.Schema.Name}/{entity.Id}/unpublish", new StringContent(string.Empty));

        response.EnsureSuccessStatusCode();

        return Result.Ok();
    }

    public async Task<Result<QueryResult<ContentItem>>> QueryAsync(ContentQuery queryParameters)
    {
        HttpResponseMessage response = await Client.PostAsJsonAsync($"api/content/query", queryParameters, ApiJsonSerializerDefault.Options);

        QueryResult<RestContentItem>? queryResult = await response.Content.ReadFromJsonAsync<QueryResult<RestContentItem>>(ApiJsonSerializerDefault.Options) ?? throw new ArgumentNullException();

        return queryResult.Convert(x => x.ToModel());
    }

    public async Task<Result<BackgroundTaskInfo>> PublishQueryAsync(ContentQuery query)
    {
        var response = await Client.PostAsJsonAsync($"api/content/publish", query, ApiJsonSerializerDefault.Options);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<BackgroundTaskInfo>(ApiJsonSerializerDefault.Options) ?? throw new ArgumentNullException();
    }

    public async Task<Result<BackgroundTaskInfo>> UnpublishQueryAsync(ContentQuery query)
    {
        var response = await Client.PostAsJsonAsync($"api/content/unpublish", query, ApiJsonSerializerDefault.Options);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<BackgroundTaskInfo>(ApiJsonSerializerDefault.Options) ?? throw new ArgumentNullException();
    }
}
