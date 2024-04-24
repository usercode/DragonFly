// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;
using Results;
using System.Net.Http.Json;

namespace DragonFly.API.Client;

/// <summary>
/// ContentService
/// </summary>
public partial class ClientContentService : IAssetFolderStorage
{
    public async Task<Result> CreateAsync(AssetFolder folder)
    {
        var response = await Client.PostAsJsonAsync("api/assetfolder", folder.ToRest(), ApiJsonSerializerDefault.Options);

        response.EnsureSuccessStatusCode();

        return Result.Ok();
    }

    public async Task<Result<AssetFolder?>> GetAssetFolderAsync(Guid id)
    {
        RestAssetFolder? entity = await Client.GetFromJsonAsync<RestAssetFolder>($"api/assetfolder/{id}", ApiJsonSerializerDefault.Options);

        if (entity == null)
        {
            return null;
        }

        return entity.ToModel();
    }

    public async Task<Result<QueryResult<AssetFolder>>> QueryAsync(AssetFolderQuery query)
    {
        var response = await Client.PostAsJsonAsync("api/assetfolder/query", query, ApiJsonSerializerDefault.Options);

        response.EnsureSuccessStatusCode();

        QueryResult<RestAssetFolder>? result = await response.Content.ReadFromJsonAsync<QueryResult<RestAssetFolder>>(ApiJsonSerializerDefault.Options) ?? throw new ArgumentNullException();

        return result.Convert(x => x.ToModel());
    }

    public Task<Result> UpdateAsync(AssetFolder folder)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DeleteAsync(AssetFolder folder)
    {
        var response = await Client.DeleteAsync($"api/assetfolder/{folder.Id}");

        response.EnsureSuccessStatusCode();

        return Result.Ok();
    }
}
