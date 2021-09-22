using DragonFly.Content;
using DragonFly.Core.Assets.Queries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly
{
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
}
