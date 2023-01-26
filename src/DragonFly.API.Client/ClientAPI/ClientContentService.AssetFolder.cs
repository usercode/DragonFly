﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Assets.Query;
using System.Net.Http.Json;

namespace DragonFly.API.Client;

/// <summary>
/// ContentService
/// </summary>
public partial class ClientContentService : IAssetFolderStorage
{
    public Task CreateAsync(AssetFolder folder)
    {
        throw new NotImplementedException();
    }

    public Task<AssetFolder> GetAssetFolderAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<AssetFolder>> GetAssetFoldersAsync(AssetFolderQuery query)
    {
        var response = await Client.PostAsJsonAsync("api/assetfolder/query", query);

        response.EnsureSuccessStatusCode();

        IEnumerable<RestAssetFolder> result = await response.Content.ReadFromJsonAsync<IEnumerable<RestAssetFolder>>();

        return result.Select(x => x.ToModel()).ToList();
    }

    public Task UpdateAsync(AssetFolder folder)
    {
        throw new NotImplementedException();
    }
}