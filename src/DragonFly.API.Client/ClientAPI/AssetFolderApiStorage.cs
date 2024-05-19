﻿// Copyright (c) usercode
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
    public AssetFolderApiStorage(HttpClient client)
    {
        Client = client;
    }

    private HttpClient Client { get; }

    public async Task<Result> CreateAsync(AssetFolder folder)
    {
        return await Client
                        .PostAsJsonAsync("api/assetfolder", folder.ToRest(), ApiJsonSerializerDefault.Options)
                        .ToResultAsync();
    }

    public async Task<Result<AssetFolder?>> GetAssetFolderAsync(Guid id)
    {
        var result = await Client
                                .GetAsync($"api/assetfolder/{id}")
                                .ToResultAsync<RestAssetFolder>(ApiJsonSerializerDefault.Options);

        return result.Convert(x => x?.ToModel());
    }

    public async Task<Result<QueryResult<AssetFolder>>> QueryAsync(AssetFolderQuery query)
    {
        var result = await Client
                                .PostAsJsonAsync("api/assetfolder/query", query, ApiJsonSerializerDefault.Options)
                                .ToResultAsync<QueryResult<RestAssetFolder>>(ApiJsonSerializerDefault.Options);

        return result.Convert(x => x.Convert(e => e.ToModel()));
    }

    public Task<Result> UpdateAsync(AssetFolder folder)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DeleteAsync(AssetFolder folder)
    {
        return await Client
                        .DeleteAsync($"api/assetfolder/{folder.Id}")
                        .ToResultAsync();
    }
}