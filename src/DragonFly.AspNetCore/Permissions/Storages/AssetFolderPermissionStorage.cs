// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License


using DragonFly.Permissions;
using Results;

namespace DragonFly.AspNetCore.Permissions;

public class AssetFolderPermissionStorage : IAssetFolderStorage
{
    public AssetFolderPermissionStorage(IAssetFolderStorage storage, IDragonFlyApi api, IPrincipalContext principalContext)
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
    private IAssetFolderStorage Storage { get; }

    public async Task<Result> CreateAsync(AssetFolder folder)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, AssetFolderPermissions.CreateAssetFolder).ThenAsync(x => Storage.CreateAsync(folder));
    }

    public async Task<Result> DeleteAsync(AssetFolder folder)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, AssetFolderPermissions.DeleteAssetFolder).ThenAsync(x => Storage.DeleteAsync(folder));
    }

    public async Task<Result<AssetFolder?>> GetAssetFolderAsync(Guid id)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, AssetFolderPermissions.ReadAssetFolder).ThenAsync(x => Storage.GetAssetFolderAsync(id));
    }

    public async Task<Result<QueryResult<AssetFolder>>> QueryAsync(AssetFolderQuery query)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, AssetFolderPermissions.QueryAssetFolder).ThenAsync(x => Storage.QueryAsync(query));
    }

    public async Task<Result> UpdateAsync(AssetFolder folder)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, AssetFolderPermissions.UpdateAssetFolder).ThenAsync(x => Storage.UpdateAsync(folder));
    }
}
