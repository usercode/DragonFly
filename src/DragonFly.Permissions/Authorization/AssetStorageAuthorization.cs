// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Assets.Query;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DragonFly.Permissions.AspNetCore.Content;

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

    public async Task ApplyMetadataAsync(Guid id)
    {
        await Api.AuthorizeAsync(AssetPermissions.AssetUpdate);

        await Storage.ApplyMetadataAsync(id);
    }

    public async Task CreateAsync(Asset asset)
    {
        await Api.AuthorizeAsync(AssetPermissions.AssetCreate);

        await Storage.CreateAsync(asset);
    }

    public async Task DeleteAsync(Guid id)
    {
        await Api.AuthorizeAsync(AssetPermissions.AssetDelete);

        await Storage.DeleteAsync(id);
    }

    public async Task<Stream> GetStreamAsync(Guid id)
    {
        await Api.AuthorizeAsync(AssetPermissions.AssetDownload);

        return await Storage.GetStreamAsync(id);
    }

    public async Task<Asset> GetAssetAsync(Guid id)
    {
        await Api.AuthorizeAsync(AssetPermissions.AssetRead);

        return await Storage.GetAssetAsync(id);
    }

    public async Task<QueryResult<Asset>> GetAssetsAsync(AssetQuery assetQuery)
    {
        await Api.AuthorizeAsync(AssetPermissions.AssetRead);

        return await Storage.GetAssetsAsync(assetQuery);
    }

    public async Task PublishAsync(Guid id)
    {
        await Api.AuthorizeAsync(AssetPermissions.AssetPublish);

        await Storage.PublishAsync(id);
    }

    public async Task UpdateAsync(Asset asset)
    {
        await Api.AuthorizeAsync(AssetPermissions.AssetUpdate);

        await Storage.UpdateAsync(asset);
    }

    public async Task UploadAsync(Guid id, string mimetype, Stream stream)
    {
        await Api.AuthorizeAsync(AssetPermissions.AssetUpload);

        await Storage.UploadAsync(id, mimetype, stream);
    }
}
