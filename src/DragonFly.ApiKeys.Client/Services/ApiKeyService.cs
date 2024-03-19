// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net.Http;
using DragonFly.API;

namespace DragonFly.ApiKeys.Razor.Services;

/// <summary>
/// ApiKeyService
/// </summary>
class ApiKeyService : IApiKeyService
{
    public ApiKeyService(HttpClient client)
    {
        Client = client;
    }

    private HttpClient Client { get; }

    public async Task CreateApiKey(ApiKey apiKey)
    {
        HttpResponseMessage response = await Client.PostAsJsonAsync("api/apikey", apiKey, ApiJsonSerializerDefault.Options);

        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateApiKey(ApiKey apiKey)
    {
        HttpResponseMessage response = await Client.PutAsJsonAsync("api/apikey", apiKey, ApiJsonSerializerDefault.Options);

        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteApiKey(ApiKey apiKey)
    {
        HttpResponseMessage response = await Client.DeleteAsync($"api/apikey/{apiKey.Id}");

        response.EnsureSuccessStatusCode();
    }

    public async Task<IEnumerable<ApiKey>> QueryApiKeys()
    {
        HttpResponseMessage response = await Client.PostAsync("api/apikey/query", new StringContent(string.Empty));

        response.EnsureSuccessStatusCode();

        IEnumerable<ApiKey>? result = await response.Content.ReadFromJsonAsync<IEnumerable<ApiKey>>(ApiJsonSerializerDefault.Options);

        if (result == null)
        {
            throw new Exception();
        }

        return result;
    }

    public Task<ApiKey> GetApiKey(string value)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiKey> GetApiKey(Guid id)
    {
        ApiKey? apikey = await Client.GetFromJsonAsync<ApiKey>($"api/apikey/{id}", ApiJsonSerializerDefault.Options);

        if (apikey == null)
        {
            throw new Exception();
        }

        return apikey;
    }        
}
