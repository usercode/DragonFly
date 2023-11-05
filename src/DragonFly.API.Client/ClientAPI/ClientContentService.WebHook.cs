// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Net.Http.Json;
using DragonFly.Query;

namespace DragonFly.API.Client;

/// <summary>
/// ContentService
/// </summary>
public partial class ClientContentService : IWebHookStorage
{
    public async Task<WebHook> GetAsync(Guid id)
    {
        var response = await Client.GetAsync($"api/webhook/{id}");

        var e = await response.Content.ReadFromJsonAsync(ApiJsonSerializerContext.Default.RestWebHook);

        return e.ToModel();
    }

    public async Task CreateAsync(WebHook entity)
    {
        var response = await Client.PostAsJsonAsync($"api/webhook", entity.ToRest(), ApiJsonSerializerContext.Default.RestWebHook);

        var result = await response.Content.ReadFromJsonAsync(ApiJsonSerializerContext.Default.ResourceCreated);

        entity.Id = result.Id;
    }

    public async Task UpdateAsync(WebHook entity)
    {
        var response = await Client.PutAsJsonAsync($"api/webhook", entity.ToRest(), ApiJsonSerializerContext.Default.RestWebHook);

        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(WebHook webHook)
    {
        await Client.DeleteAsync($"api/webhook/{webHook.Id}");
    }

    public async Task<QueryResult<WebHook>> QueryAsync(WebHookQuery query)
    {
        var response = await Client.PostAsJsonAsync("api/webhook/query", query, ApiJsonSerializerContext.Default.WebHookQuery);

        QueryResult<RestWebHook>? result = await response.Content.ReadFromJsonAsync(ApiJsonSerializerContext.Default.QueryResultRestWebHook);

        return result.Convert(x => x.ToModel());
    }
}
