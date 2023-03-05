// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Assets.Query;
using DragonFly.Query;
using System.Net.Http.Json;

namespace DragonFly.API.Client;

/// <summary>
/// ContentService
/// </summary>
public partial class ClientContentService : IAssetFolderStorage
{
    public async Task CreateAsync(AssetFolder folder)
    {
        var response = await Client.PostAsJsonAsync("api/assetfolder", folder.ToRest());

        response.EnsureSuccessStatusCode();
    }

    public async Task<AssetFolder?> GetAssetFolderAsync(Guid id)
    {
        RestAssetFolder? entity = await Client.GetFromJsonAsync<RestAssetFolder>($"api/assetfolder/{id}");

        if (entity == null)
        {
            return null;
        }

        return entity.ToModel();
    }

    public async Task<QueryResult<AssetFolder>> QueryAsync(AssetFolderQuery query)
    {
        var response = await Client.PostAsJsonAsync("api/assetfolder/query", query);

        response.EnsureSuccessStatusCode();

        QueryResult<RestAssetFolder>? result = await response.Content.ReadFromJsonAsync<QueryResult<RestAssetFolder>>();

        return result.Convert(x => x.ToModel());
    }

    public Task UpdateAsync(AssetFolder folder)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(AssetFolder folder)
    {
        var response = await Client.DeleteAsync($"api/assetfolder/{folder.Id}");

        response.EnsureSuccessStatusCode();
    }
}
