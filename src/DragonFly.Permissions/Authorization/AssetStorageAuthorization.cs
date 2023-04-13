// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Assets.Query;
using DragonFly.Query;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DragonFly.Permissions;

/// <summary>
/// AssetStorageAuthorization
/// </summary>
class AssetStorageAuthorization : IAssetStorage
{
    public AssetStorageAuthorization(
        IAssetStorage storage,
        IDragonFlyApi api)
    {
        Api = api;
        Storage = storage;
    }

    /// <summary>
    /// Storage
    /// </summary>
    public IAssetStorage Storage { get; }

    /// <summary>
    /// Authorization
    /// </summary>
    public IDragonFlyApi Api { get; }

    public async Task ApplyMetadataAsync(Asset asset)
    {
        await Api.AuthorizeAsync(AssetPermissions.AssetUpdate);

        await Storage.ApplyMetadataAsync(asset);
    }

    public async Task<BackgroundTaskInfo> ApplyMetadataAsync(AssetQuery query)
    {
        await Api.AuthorizeAsync(AssetPermissions.AssetUpdate);

        return await Storage.ApplyMetadataAsync(query);
    }

    public async Task CreateAsync(Asset asset)
    {
        await Api.AuthorizeAsync(AssetPermissions.AssetCreate);

        await Storage.CreateAsync(asset);
    }

    public async Task DeleteAsync(Asset asset)
    {
        await Api.AuthorizeAsync(AssetPermissions.AssetDelete);

        await Storage.DeleteAsync(asset);
    }

    public async Task<Stream> GetStreamAsync(Asset asset)
    {
        await Api.AuthorizeAsync(AssetPermissions.AssetDownload);

        return await Storage.GetStreamAsync(asset);
    }

    public async Task<Asset?> GetAssetAsync(Guid id)
    {
        await Api.AuthorizeAsync(AssetPermissions.AssetRead);

        return await Storage.GetAssetAsync(id);
    }

    public async Task<QueryResult<Asset>> QueryAsync(AssetQuery assetQuery)
    {
        await Api.AuthorizeAsync(AssetPermissions.AssetRead);

        return await Storage.QueryAsync(assetQuery);
    }

    public async Task PublishAsync(Asset asset)
    {
        await Api.AuthorizeAsync(AssetPermissions.AssetPublish);

        await Storage.PublishAsync(asset);
    }

    public async Task UpdateAsync(Asset asset)
    {
        await Api.AuthorizeAsync(AssetPermissions.AssetUpdate);

        await Storage.UpdateAsync(asset);
    }

    public async Task UploadAsync(Asset asset, string mimetype, Stream stream)
    {
        await Api.AuthorizeAsync(AssetPermissions.AssetUpload);

        await Storage.UploadAsync(asset, mimetype, stream);
    }
}
