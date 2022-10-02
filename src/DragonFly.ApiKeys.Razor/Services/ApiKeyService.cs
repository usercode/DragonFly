// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFLy.ApiKeys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net.Http;

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
        HttpResponseMessage response = await Client.PostAsJsonAsync("apikey", apiKey);

        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateApiKey(ApiKey apiKey)
    {
        HttpResponseMessage response = await Client.PutAsJsonAsync("apikey", apiKey);

        response.EnsureSuccessStatusCode();
    }

    public Task DeleteApiKey(ApiKey apiKey)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ApiKey>> GetAllApiKeys()
    {
        HttpResponseMessage response = await Client.PostAsync("apikey/query", new StringContent(string.Empty) );

        response.EnsureSuccessStatusCode();

        IEnumerable<ApiKey>? result = await response.Content.ReadFromJsonAsync<IEnumerable<ApiKey>>();

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
        ApiKey? apikey = await Client.GetFromJsonAsync<ApiKey>($"apikey/{id}");

        if (apikey == null)
        {
            throw new Exception();
        }

        return apikey;
    }        
}
