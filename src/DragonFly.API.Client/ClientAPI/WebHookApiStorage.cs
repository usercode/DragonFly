// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Net.Http.Json;
using DragonFly.Query;
using SmartResults;

namespace DragonFly.API.Client;

/// <summary>
/// WebHookStorage
/// </summary>
internal class WebHookApiStorage : IWebHookStorage
{
    public WebHookApiStorage(HttpClient client)
    {
        Client = client;
    }

    private HttpClient Client { get; }

    public async Task<Result<WebHook?>> GetAsync(Guid id)
    {
        return (await Client
                        .GetAsync($"api/webhook/{id}")
                        .ReadResultFromJsonAsync<RestWebHook>(ApiJsonSerializerDefault.Options))
                        .ToResult(x => x?.ToModel());
    }

    public async Task<Result> CreateAsync(WebHook entity)
    {
        var result = await Client
                                .PostAsJsonAsync($"api/webhook", entity.ToRest(), ApiJsonSerializerDefault.Options)
                                .ReadResultFromJsonAsync<ResourceCreated>(ApiJsonSerializerDefault.Options);

        if (result.IsSucceeded)
        {
            entity.Id = result.Value.Id;
        }

        return result;
    }

    public async Task<Result> UpdateAsync(WebHook entity)
    {
        return await Client
                        .PutAsJsonAsync($"api/webhook", entity.ToRest(), ApiJsonSerializerDefault.Options)
                        .ReadResultFromJsonAsync();
    }

    public async Task<Result> DeleteAsync(WebHook webHook)
    {
        return await Client
                        .DeleteAsync($"api/webhook/{webHook.Id}")
                        .ReadResultFromJsonAsync();
    }

    public async Task<Result<QueryResult<WebHook>>> QueryAsync(WebHookQuery query)
    {
        return (await Client
                        .PostAsJsonAsync("api/webhook/query", query, ApiJsonSerializerDefault.Options)
                        .ReadResultFromJsonAsync<QueryResult<RestWebHook>>(ApiJsonSerializerDefault.Options))
                        .ToResult(x => x.Convert(e => e.ToModel()));
    }
}
