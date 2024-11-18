// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;
using SmartResults;
using System.Net.Http.Json;

namespace DragonFly.API.Client;

/// <summary>
/// AssetFolderStorage
/// </summary>
internal class AssetFolderApiStorage : IAssetFolderStorage
{
    public AssetFolderApiStorage(RestApiClient client)
    {
        Client = client.Http;
    }

    private HttpClient Client { get; }

    public async Task<Result> CreateAsync(AssetFolder folder)
    {
        return await Client
                        .PostAsJsonAsync("api/assetfolder", folder.ToRest(), ApiJsonSerializerDefault.Options)
                        .ReadResultFromJsonAsync(ApiJsonSerializerDefault.Options);
    }

    public async Task<Result<AssetFolder?>> GetAssetFolderAsync(Guid id)
    {
        var result = await Client
                                .GetAsync($"api/assetfolder/{id}")
                                .ReadResultFromJsonAsync<RestAssetFolder>(ApiJsonSerializerDefault.Options);

        return result.ToResult(x => x?.ToModel());
    }

    public async Task<Result<QueryResult<AssetFolder>>> QueryAsync(AssetFolderQuery query)
    {
        var result = await Client
                                .PostAsJsonAsync("api/assetfolder/query", query, ApiJsonSerializerDefault.Options)
                                .ReadResultFromJsonAsync<QueryResult<RestAssetFolder>>(ApiJsonSerializerDefault.Options);

        return result.ToResult(x => x.Convert(e => e.ToModel()));
    }

    public Task<Result> UpdateAsync(AssetFolder folder)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DeleteAsync(AssetFolder folder)
    {
        return await Client
                        .DeleteAsync($"api/assetfolder/{folder.Id}")
                        .ReadResultFromJsonAsync(ApiJsonSerializerDefault.Options);
    }
}
