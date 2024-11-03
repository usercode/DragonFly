// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Net.Http.Json;
using DragonFly.Query;
using SmartResults;

namespace DragonFly.API.Client;

/// <summary>
/// ContentSchemaApiStorage
/// </summary>
internal class ContentSchemaApiStorage : ISchemaStorage
{
    public ContentSchemaApiStorage(HttpClient client)
    {
        Client = client;
    }

    private HttpClient Client { get; }

    public async Task<Result<ContentSchema?>> GetSchemaAsync(Guid id)
    {
        var result = await Client
                            .GetAsync($"api/schema/{id}")
                            .ReadResultFromJsonAsync<RestContentSchema>(ApiJsonSerializerDefault.Options);

        return result.ToResult(x => x?.ToModel());
    }

    public async Task<Result<ContentSchema?>> GetSchemaAsync(string name)
    {
        var result = await Client
                            .GetAsync($"api/schema/{name}")
                            .ReadResultFromJsonAsync<RestContentSchema>(ApiJsonSerializerDefault.Options);

        return result.ToResult(x => x?.ToModel());
    }

    public async Task<Result> CreateAsync(ContentSchema entity)
    {
        var result = await Client
                                .PostAsJsonAsync($"api/schema", entity.ToRest(), ApiJsonSerializerDefault.Options)
                                .ReadResultFromJsonAsync<ResourceCreated>(ApiJsonSerializerDefault.Options);

        if (result.IsSucceeded)
        {
            entity.Id = result.Value.Id;
        }

        return result;
    }

    public async Task<Result> UpdateAsync(ContentSchema entity)
    {
        return await Client
                        .PutAsJsonAsync($"api/schema", entity.ToRest(), ApiJsonSerializerDefault.Options)
                        .ReadResultFromJsonAsync();
    }

    public async Task<Result> DeleteAsync(ContentSchema entity)
    {
        return await Client
                        .DeleteAsync($"api/schema/{entity.Id}")
                        .ReadResultFromJsonAsync();
    }

    public async Task<Result<QueryResult<ContentSchema>>> QuerySchemasAsync()
    {
        var result = await Client
                                .PostAsync("api/schema/query", new StringContent(string.Empty))
                                .ReadResultFromJsonAsync<QueryResult<RestContentSchema>>(ApiJsonSerializerDefault.Options);

        return result.ToResult(x => x.Convert(x => x.ToModel()));
    }
}
