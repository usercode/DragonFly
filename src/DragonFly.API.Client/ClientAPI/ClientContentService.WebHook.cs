// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Net.Http.Json;
using DragonFly.Query;
using SmartResults;

namespace DragonFly.API.Client;

/// <summary>
/// ContentService
/// </summary>
public partial class ClientContentService : IWebHookStorage
{
    public async Task<Result<WebHook?>> GetAsync(Guid id)
    {
        var response = await Client.GetAsync($"api/webhook/{id}");

        return await response.ToResultAsync<RestWebHook>(ApiJsonSerializerDefault.Options)
                                .ThenAsync(x => Result.Ok(x.Value.ToModel()));
    }

    public async Task<Result> CreateAsync(WebHook entity)
    {
        var response = await Client.PostAsJsonAsync($"api/webhook", entity.ToRest(), ApiJsonSerializerDefault.Options);

        var result = await response.Content.ReadFromJsonAsync<ResourceCreated>(ApiJsonSerializerDefault.Options) ?? throw new ArgumentNullException();

        entity.Id = result.Id;

        return Result.Ok();
    }

    public async Task<Result> UpdateAsync(WebHook entity)
    {
        var response = await Client.PutAsJsonAsync($"api/webhook", entity.ToRest(), ApiJsonSerializerDefault.Options);

        return await response.ToResultAsync();
    }

    public async Task<Result> DeleteAsync(WebHook webHook)
    {
        var response = await Client.DeleteAsync($"api/webhook/{webHook.Id}");

        return await response.ToResultAsync();
    }

    public async Task<Result<QueryResult<WebHook>>> QueryAsync(WebHookQuery query)
    {
        var response = await Client.PostAsJsonAsync("api/webhook/query", query, ApiJsonSerializerDefault.Options);

        QueryResult<RestWebHook>? result = await response.Content.ReadFromJsonAsync<QueryResult<RestWebHook>>(ApiJsonSerializerDefault.Options) ?? throw new ArgumentNullException();

        return result.Convert(x => x.ToModel());
    }
}
