// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// IAssetFolderStorage
/// </summary>
public interface IAssetFolderStorage
{
    Task<AssetFolder?> GetAssetFolderAsync(Guid id);

    Task<QueryResult<AssetFolder>> QueryAsync(AssetFolderQuery query);

    Task CreateAsync(AssetFolder folder);

    Task UpdateAsync(AssetFolder folder);

    Task DeleteAsync(AssetFolder folder);
}
