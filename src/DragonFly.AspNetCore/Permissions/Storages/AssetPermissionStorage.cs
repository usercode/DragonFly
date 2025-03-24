// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Permissions;
using SmartResults;

namespace DragonFly.AspNetCore.Permissions;

public class AssetPermissionStorage : IAssetStorage
{
    public AssetPermissionStorage(IAssetStorage storage, IDragonFlyApi api, IPrincipalContext principalContext)
    {
        Storage = storage;
        Api = api;
        PrincipalContext = principalContext;
    }

    /// <summary>
    /// Api
    /// </summary>
    private IDragonFlyApi Api { get; }

    /// <summary>
    /// PrincipalContext
    /// </summary>
    private IPrincipalContext PrincipalContext { get; }

    /// <summary>
    /// Storage
    /// </summary>
    private IAssetStorage Storage { get; }

    public async Task<Result> ApplyMetadataAsync(Asset asset)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, AssetPermissions.UpdateAsset)
                        .ThenAsync(x => Storage.ApplyMetadataAsync(asset))
                        .ConfigureAwait(false);
    }

    public async Task<Result<BackgroundTaskInfo>> ApplyMetadataAsync(AssetQuery query)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, AssetPermissions.UpdateAsset)
                        .ThenAsync(x => Storage.ApplyMetadataAsync(query))
                        .ConfigureAwait(false);
    }

    public async Task<Result> CreateAsync(Asset asset)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, AssetPermissions.CreateAsset)
                        .ThenAsync(x => Storage.CreateAsync(asset))
                        .ConfigureAwait(false);
    }

    public async Task<Result> DeleteAsync(Asset asset)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, AssetPermissions.DeleteAsset)
                        .ThenAsync(x => Storage.DeleteAsync(asset))
                        .ConfigureAwait(false);
    }

    public async Task<Result<Asset?>> GetAssetAsync(Guid id)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, AssetPermissions.ReadAsset)
                        .ThenAsync(x  => Storage.GetAssetAsync(id))
                        .ConfigureAwait(false);
    }

    public async Task<Result<Stream>> OpenStreamAsync(Guid assetId)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, AssetPermissions.DownloadAsset)
                        .ThenAsync(x => Storage.OpenStreamAsync(assetId))
                        .ConfigureAwait(false);
    }

    public async Task<Result> PublishAsync(Asset asset)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, AssetPermissions.PublishAsset)
                        .ThenAsync(x => Storage.PublishAsync(asset))
                        .ConfigureAwait(false);
    }

    public async Task<Result<QueryResult<Asset>>> QueryAsync(AssetQuery query)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, AssetPermissions.QueryAsset)
                        .ThenAsync(x => Storage.QueryAsync(query))
                        .ConfigureAwait(false);
    }

    public async Task<Result> UpdateAsync(Asset asset)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, AssetPermissions.UpdateAsset)
                        .ThenAsync(x => Storage.UpdateAsync(asset))
                        .ConfigureAwait(false);
    }

    public async Task<Result> UploadAsync(Guid assetId, string mimetype, Stream stream)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, AssetPermissions.UploadAsset)
                      .ThenAsync(x => Storage.UploadAsync(assetId, mimetype, stream))
                      .ConfigureAwait(false);
    }
}
