// Copyright (c) usercode
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
    public AssetApiStorage(RestApiClient restClient)
    {
        Client = restClient.HttpClient;
    }

    private HttpClient Client { get; }

    public async Task<Result<QueryResult<Asset>>> QueryAsync(AssetQuery assetQuery)
    {
        var result = await Client
                                .PostAsJsonAsync($"api/asset/query", assetQuery, ApiJsonSerializerDefault.Options)
                                .ReadResultFromJsonAsync<QueryResult<RestAsset>>(ApiJsonSerializerDefault.Options);

        return result.ToResult(x => x.Convert(e => e.ToModel()));
    }

    public async Task<Result> UploadAsync(Guid assetId, string mimetype, Stream stream)
    {
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"api/asset/{assetId}/upload");
        requestMessage.Content = new StreamContent(stream);
        requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(mimetype);

        return await Client.SendAsync(requestMessage).ReadResultFromJsonAsync();
    }

    public async Task<Result<Stream>> GetStreamAsync(Guid assetId)
    {
        return await Client.GetStreamAsync($"api/asset/{assetId}/download");
    }

    public async Task<Result<Asset?>> GetAssetAsync(Guid id)
    {
        var result = await Client
                                .GetAsync($"api/asset/{id}")
                                .ReadResultFromJsonAsync<RestAsset>(ApiJsonSerializerDefault.Options);

        return result.ToResult(x => x?.ToModel());
    }

    public async Task<Result> CreateAsync(Asset entity)
    {
        var result = await Client
                            .PostAsJsonAsync($"api/asset", entity.ToRest(), ApiJsonSerializerDefault.Options)
                            .ReadResultFromJsonAsync<ResourceCreated>(ApiJsonSerializerDefault.Options);

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
                        .ReadResultFromJsonAsync(ApiJsonSerializerDefault.Options);
    }

    public async Task<Result> UpdateAsync(Asset entity)
    {
        return await Client
                        .PutAsJsonAsync($"api/asset", entity.ToRest(), ApiJsonSerializerDefault.Options)
                        .ReadResultFromJsonAsync(ApiJsonSerializerDefault.Options);
    }

    public async Task<Result> DeleteAsync(Asset asset)
    {
        return await Client
                        .DeleteAsync($"api/asset/{asset.Id}")
                        .ReadResultFromJsonAsync(ApiJsonSerializerDefault.Options);
    }

    public async Task<Result> ApplyMetadataAsync(Asset asset)
    {
        return await Client
                        .PostAsync($"api/asset/{asset.Id}/metadata", new StringContent(string.Empty))
                        .ReadResultFromJsonAsync(ApiJsonSerializerDefault.Options);
    }

    public async Task<Result<BackgroundTaskInfo>> ApplyMetadataAsync(AssetQuery query)
    {
        return await Client
                        .PostAsJsonAsync($"api/asset/metadata", query, ApiJsonSerializerDefault.Options)
                        .ReadResultFromJsonAsync<BackgroundTaskInfo>(ApiJsonSerializerDefault.Options);
    }
}
