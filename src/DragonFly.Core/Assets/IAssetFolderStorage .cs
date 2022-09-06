using DragonFly.Assets.Query;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

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
