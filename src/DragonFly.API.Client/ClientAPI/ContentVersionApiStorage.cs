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
    public ContentVersionApiStorage(HttpClient client)
    {
        Client = client;
    }

    private HttpClient Client { get; }

    public async Task<Result<IEnumerable<ContentVersionEntry>>> GetContentVersionsAsync(string schema, Guid id)
    {
        QueryResult<ContentVersionEntry>? result = await Client.GetFromJsonAsync<QueryResult<ContentVersionEntry>>($"api/content/{schema}/{id}/versions", ApiJsonSerializerDefault.Options);

        if (result == null)
        {
            return Result.Ok(Enumerable.Empty<ContentVersionEntry>());
        }

        return Result.Ok(result.Items.AsEnumerable());
    }

    public async Task<Result<ContentItem?>> GetContentByVersionAsync(string schema, Guid id)
    {
        var result = await Client
                             .GetAsync($"api/content/{schema}/{id}/version")
                             .ReadResultFromJsonAsync<RestContentItem>(ApiJsonSerializerDefault.Options);

        return result.ToResult(x => x?.ToModel());
    }
}
