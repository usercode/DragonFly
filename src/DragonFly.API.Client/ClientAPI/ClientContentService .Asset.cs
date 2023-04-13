﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Net.Http.Json;
using System.Net.Http.Headers;
using DragonFly.Assets.Query;
using DragonFly.Query;

namespace DragonFly.API.Client;

/// <summary>
/// ContentService
/// </summary>
public partial class ClientContentService : IAssetStorage
{
    public async Task<QueryResult<Asset>> QueryAsync(AssetQuery assetQuery)
    {
        var response = await Client.PostAsJsonAsync($"api/asset/query", assetQuery);

        var result = await response.Content.ReadFromJsonAsync<QueryResult<RestAsset>>();

        if (result == null)
        {
            throw new Exception();
        }

        return result.Convert(x => x.ToModel());
    }

    public async Task UploadAsync(Asset asset, string mimetype, Stream stream)
    {
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"api/asset/{asset.Id}/upload");
        requestMessage.Content = new StreamContent(stream);
        requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(mimetype);

        var response = await Client.SendAsync(requestMessage);

        response.EnsureSuccessStatusCode();
    }

    public async Task<Stream> GetStreamAsync(Asset asset)
    {
        return await Client.GetStreamAsync($"api/asset/{asset.Id}/download");
    }

    //assets
    public async Task<Asset> GetAssetAsync(Guid id)
    {
        var response = await Client.GetAsync($"api/asset/{id}");

        RestAsset? restAsset = await response.Content.ReadFromJsonAsync<RestAsset>();

        return restAsset.ToModel();
    }

    public async Task CreateAsync(Asset entity)
    {
        var response = await Client.PostAsJsonAsync($"api/asset", entity.ToRest());

        var result = await response.Content.ReadFromJsonAsync<ResourceCreated>();

        entity.Id = result.Id;
    }

    public async Task PublishAsync(Asset asset)
    {
        await Client.PostAsync($"api/asset/{asset.Id}/publish", new StringContent(string.Empty));
    }

    public async Task UpdateAsync(Asset entity)
    {
        await Client.PutAsJsonAsync($"api/asset", entity.ToRest());
    }

    public async Task DeleteAsync(Asset asset)
    {
        await Client.DeleteAsync($"api/asset/{asset.Id}");
    }

    public async Task ApplyMetadataAsync(Asset asset)
    {
        await Client.PostAsync($"api/asset/{asset.Id}/metadata", new StringContent(string.Empty));
    }

    public async Task<BackgroundTaskInfo> ApplyMetadataAsync(AssetQuery query)
    {
        var response = await Client.PostAsJsonAsync($"api/asset/metadata", query);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<BackgroundTaskInfo>();
    }
}
