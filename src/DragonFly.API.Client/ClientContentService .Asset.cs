// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Exports;
using DragonFly.AspNetCore.API.Models.Assets;
using DragonFly.Data.Models;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using DragonFly.Storage;
using DragonFly.Assets.Query;

namespace DragonFly.Client;

/// <summary>
/// ContentService
/// </summary>
public partial class ClientContentService : IAssetStorage
{
    public async Task<QueryResult<Asset>> GetAssetsAsync(AssetQuery assetQuery)
    {
        var response = await Client.PostAsJsonAsync($"api/asset/query", assetQuery);

        var result = await response.Content.ReadFromJsonAsync<QueryResult<RestAsset>>();

        if (result == null)
        {
            throw new Exception();
        }

        QueryResult<Asset> assetResult = new QueryResult<Asset>();
        assetResult.Offset = result.Offset;
        assetResult.Count = result.Count;
        assetResult.TotalCount = result.TotalCount;
        assetResult.Items = result.Items.Select(x => x.ToModel()).ToList();

        return assetResult;
    }

    public async Task UploadAsync(Guid id, string mimetype, Stream stream)
    {
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"api/asset/{id}/upload");
        requestMessage.Content = new StreamContent(stream);
        requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(mimetype);

        var response = await Client.SendAsync(requestMessage);

        response.EnsureSuccessStatusCode();
    }

    public async Task<Stream> DownloadAsync(Guid id)
    {
        return await Client.GetStreamAsync($"api/asset/{id}/download");
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

    public async Task PublishAsync(Guid id)
    {
        await Client.PostAsync($"api/asset/{id}/publish", new StringContent(string.Empty));
    }

    public async Task UpdateAsync(Asset entity)
    {
        await Client.PutAsJsonAsync($"api/asset", entity.ToRest());
    }

    public async Task DeleteAsync(Guid id)
    {
        await Client.DeleteAsync($"api/asset/{id}");
    }

    public async Task ApplyMetadataAsync(Guid id)
    {
        await Client.PostAsync($"api/asset/{id}/metadata", new StringContent(string.Empty));
    }
}
