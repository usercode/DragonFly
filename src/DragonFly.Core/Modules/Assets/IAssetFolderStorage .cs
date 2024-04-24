// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Results;

namespace DragonFly;

/// <summary>
/// IAssetFolderStorage
/// </summary>
public interface IAssetFolderStorage
{
    Task<Result<AssetFolder?>> GetAssetFolderAsync(Guid id);

    Task<Result<QueryResult<AssetFolder>>> QueryAsync(AssetFolderQuery query);

    Task<Result> CreateAsync(AssetFolder folder);

    Task<Result> UpdateAsync(AssetFolder folder);

    Task<Result> DeleteAsync(AssetFolder folder);
}
