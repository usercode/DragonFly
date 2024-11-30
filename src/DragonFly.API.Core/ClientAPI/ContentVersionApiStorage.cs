// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Net.Http.Json;
using SmartResults;

namespace DragonFly.API.Client;

/// <summary>
/// ContentVersionStorage
/// </summary>
internal class ContentVersionApiStorage : IContentVersionStorage
{
    public ContentVersionApiStorage(RestApiClient restClient)
    {
        Client = restClient.HttpClient;
    }

    private HttpClient Client { get; }

    public async Task<Result<QueryResult<ContentVersionEntry>>> GetContentVersionsAsync(string schema, Guid id)
    {
        var result = await Client.GetFromJsonAsync<Result<QueryResult<ContentVersionEntry>>>($"api/content/{schema}/{id}/versions", ApiJsonSerializerDefault.Options);

        return result;
    }

    public async Task<Result<ContentItem?>> GetContentByVersionAsync(string schema, Guid id)
    {
        var result = await Client
                             .GetAsync($"api/content/{schema}/{id}/version")
                             .ReadResultFromJsonAsync<RestContentItem>(ApiJsonSerializerDefault.Options);

        return result.ToResult(x => x?.ToModel());
    }
}
