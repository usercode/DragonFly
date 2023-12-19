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

        var e = await response.Content.ReadFromJsonAsync<RestWebHook>(ApiJsonSerializerDefault.Options);

        return e.ToModel();
    }

    public async Task CreateAsync(WebHook entity)
    {
        var response = await Client.PostAsJsonAsync($"api/webhook", entity.ToRest(), ApiJsonSerializerDefault.Options);

        var result = await response.Content.ReadFromJsonAsync<ResourceCreated>(ApiJsonSerializerDefault.Options);

        entity.Id = result.Id;
    }

    public async Task UpdateAsync(WebHook entity)
    {
        var response = await Client.PutAsJsonAsync($"api/webhook", entity.ToRest(), ApiJsonSerializerDefault.Options);

        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(WebHook webHook)
    {
        await Client.DeleteAsync($"api/webhook/{webHook.Id}");
    }

    public async Task<QueryResult<WebHook>> QueryAsync(WebHookQuery query)
    {
        var response = await Client.PostAsJsonAsync("api/webhook/query", query, ApiJsonSerializerDefault.Options);

        QueryResult<RestWebHook>? result = await response.Content.ReadFromJsonAsync<QueryResult<RestWebHook>>(ApiJsonSerializerDefault.Options);

        return result.Convert(x => x.ToModel());
    }
}
