﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Assets.Query;

namespace DragonFly.Storage;

/// <summary>
/// IAssetFolderStorage
/// </summary>
public interface IAssetFolderStorage
{
    Task<AssetFolder> GetAssetFolderAsync(Guid id);

    Task<IEnumerable<AssetFolder>> GetAssetFoldersAsync(AssetFolderQuery query);

    Task CreateAsync(AssetFolder folder);

    Task UpdateAsync(AssetFolder folder);
}
