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
        return await Client
                        .GetAsync($"api/webhook/{id}")
                        .ToResultAsync<RestWebHook>(ApiJsonSerializerDefault.Options)
                        .ConvertAsync(x => x?.ToModel());
    }

    public async Task<Result> CreateAsync(WebHook entity)
    {
        var result = await Client
                                .PostAsJsonAsync($"api/webhook", entity.ToRest(), ApiJsonSerializerDefault.Options)
                                .ToResultAsync<ResourceCreated>(ApiJsonSerializerDefault.Options);

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
                        .ToResultAsync();
    }

    public async Task<Result> DeleteAsync(WebHook webHook)
    {
        return await Client
                        .DeleteAsync($"api/webhook/{webHook.Id}")
                        .ToResultAsync();
    }

    public async Task<Result<QueryResult<WebHook>>> QueryAsync(WebHookQuery query)
    {
        return await Client
                        .PostAsJsonAsync("api/webhook/query", query, ApiJsonSerializerDefault.Options)
                        .ToResultAsync<QueryResult<RestWebHook>>(ApiJsonSerializerDefault.Options)
                        .ConvertAsync(x => x.Convert(e => e.ToModel()));
    }
}
