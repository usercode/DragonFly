﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Net.Http.Json;
using System.Net.Http.Headers;
using DragonFly.Query;
using SmartResults;

namespace DragonFly.API.Client;

/// <summary>
/// AssetStorage
/// </summary>
internal class AssetApiStorage : IAssetStorage
{
    public AssetApiStorage(HttpClient client)
    {
        Client = client;
    }

    private HttpClient Client { get; }

    public async Task<Result<QueryResult<Asset>>> QueryAsync(AssetQuery assetQuery)
    {
        var result = await Client
                                .PostAsJsonAsync($"api/asset/query", assetQuery, ApiJsonSerializerDefault.Options)
                                .ToResultAsync<QueryResult<RestAsset>>(ApiJsonSerializerDefault.Options);

        return result.Convert(x => x.Convert(e => e.ToModel()));
    }

    public async Task<Result> UploadAsync(Asset asset, string mimetype, Stream stream)
    {
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"api/asset/{asset.Id}/upload");
        requestMessage.Content = new StreamContent(stream);
        requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(mimetype);

        return await Client.SendAsync(requestMessage).ToResultAsync();
    }

    public async Task<Result<Stream>> GetStreamAsync(Asset asset)
    {
        return await Client.GetStreamAsync($"api/asset/{asset.Id}/download");
    }

    //assets
    public async Task<Result<Asset?>> GetAssetAsync(Guid id)
    {
        var result = await Client
                                .GetAsync($"api/asset/{id}")
                                .ToResultAsync<RestAsset>(ApiJsonSerializerDefault.Options);

        return result.Convert(x => x?.ToModel());
    }

    public async Task<Result> CreateAsync(Asset entity)
    {
        var result = await Client
                            .PostAsJsonAsync($"api/asset", entity.ToRest())
                            .ToResultAsync<ResourceCreated>(ApiJsonSerializerDefault.Options);

        if (result.IsSucceeded)
        {
            entity.Id = result.Value.Id;
        }

        return result;
    }

    public async Task<Result> PublishAsync(Asset asset)
    {
        return await Client
                        .PostAsync($"api/asset/{asset.Id}/publish", new StringContent(string.Empty))
                        .ToResultAsync();
    }

    public async Task<Result> UpdateAsync(Asset entity)
    {
        return await Client
                        .PutAsJsonAsync($"api/asset", entity.ToRest(), ApiJsonSerializerDefault.Options)
                        .ToResultAsync();
    }

    public async Task<Result> DeleteAsync(Asset asset)
    {
        return await Client
                        .DeleteAsync($"api/asset/{asset.Id}")
                        .ToResultAsync();
    }

    public async Task<Result> ApplyMetadataAsync(Asset asset)
    {
        return await Client
                        .PostAsync($"api/asset/{asset.Id}/metadata", new StringContent(string.Empty))
                        .ToResultAsync();
    }

    public async Task<Result<BackgroundTaskInfo>> ApplyMetadataAsync(AssetQuery query)
    {
        return await Client
                        .PostAsJsonAsync($"api/asset/metadata", query, ApiJsonSerializerDefault.Options)
                        .ToResultAsync<BackgroundTaskInfo>(ApiJsonSerializerDefault.Options);
    }
}