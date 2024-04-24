// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License


using DragonFly.Permissions;
using Results;

namespace DragonFly.AspNetCore.Permissions;

public class AssetFolderPermissionStorage : IAssetFolderStorage
{
    public AssetFolderPermissionStorage(IAssetFolderStorage storage, IDragonFlyApi api)
    {
        Storage = storage;
        Api = api;
    }

    /// <summary>
    /// Api
    /// </summary>
    private IDragonFlyApi Api { get; }

    /// <summary>
    /// Storage
    /// </summary>
    private IAssetFolderStorage Storage { get; }

    public async Task<Result> CreateAsync(AssetFolder folder)
    {
        return await Api.AuthorizeAsync(AssetFolderPermissions.CreateAssetFolder).ThenAsync(x => Storage.CreateAsync(folder));
    }

    public async Task<Result> DeleteAsync(AssetFolder folder)
    {
        return await Api.AuthorizeAsync(AssetFolderPermissions.DeleteAssetFolder).ThenAsync(x => Storage.DeleteAsync(folder));
    }

    public async Task<Result<AssetFolder?>> GetAssetFolderAsync(Guid id)
    {
        return await Api.AuthorizeAsync(AssetFolderPermissions.ReadAssetFolder).ThenAsync(x => Storage.GetAssetFolderAsync(id));
    }

    public async Task<Result<QueryResult<AssetFolder>>> QueryAsync(AssetFolderQuery query)
    {
        return await Api.AuthorizeAsync(AssetFolderPermissions.QueryAssetFolder).ThenAsync(x => Storage.QueryAsync(query));
    }

    public async Task<Result> UpdateAsync(AssetFolder folder)
    {
        return await Api.AuthorizeAsync(AssetFolderPermissions.UpdateAssetFolder).ThenAsync(x => Storage.UpdateAsync(folder));
    }
}
